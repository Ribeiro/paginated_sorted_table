//Autores: Geovanny Ribeiro e Emerson Barbosa

function PaginatedSortedTable(json) {
    json.sortingClass = json.order == "ASC" ? "headerSortUp" : "headerSortDown";
    json.headerSortUpClass = "headerSortUp";
    json.headerSortDownClass = "headerSortDown";
    json.ajaxNotFoundResource = "Ajax URL invalid! Please, check if your urlAjax value is valid!";
    json.imageOrderUp = json.imagesPath + "order_up.png";
    json.imageOrderDown = json.imagesPath + "order_down.png";
    json.imageOrderNone = json.imagesPath + "order_none.png";

    /* sera alterado para tratamento dinamico das imagens */
    this.notOrdenedThs = $("#" + json.tableId + " th").each(function (index, th) {
        if ($(this).attr("id") && $(this).attr("id") != json.header) {
            $(this).html('<img src=' + json.imageOrderNone + ' alt="Ordenação" />' + $(this).html());
        }
        if ($(this).attr("id")) {
            $(this).css('cursor', 'pointer');
        }
    });

	this.ordenedTh = $("#" + json.tableId + " #" + json.header);
	this.ordenedTh.addClass(json.sortingClass);

	if (this.ordenedTh.hasClass(json.headerSortUpClass)) {
	    this.ordenedTh.html('<img src=' + json.imageOrderUp + ' alt="Ordenação" />' + this.ordenedTh.html());
	}
	else if (this.ordenedTh.hasClass(json.headerSortDownClass)) {
	    this.ordenedTh.html('<img src=' + json.imageOrderDown + ' alt="Ordenação" />' + this.ordenedTh.html());
	}
	/* end */

	function checkForValidOrder(order) {
	    var orderUpRegex = /^[aA][sS][cC]$/;
	    var orderDownUpRegex = /^[dD][eE][sS][cC]$/;
	    var invalidOrderMessage = "Please, provide a valid order! Only 'ASC' or 'DESC' are accepted!";

	    if (!order.test(orderUpRegex) || !order.test(orderDownUpRegex)) {
	        alert(invalidOrderMessage);
	        return false;
	    }

	}
    
	function handleHeaderClick(hdr, json) {
		var headerVal = $(hdr).attr("id");
		var dataOrder = "ASC";

		if (headerVal) {

			if ($(hdr).hasClass(json.headerSortDownClass) == true) {
			    $(hdr).removeClass(json.headerSortDownClass);
			    $(hdr).addClass(json.headerSortUpClass);

			} else if ($(hdr).hasClass(json.headerSortUpClass) == true) {
			    dataOrder = "DESC";
			    $(hdr).removeClass(json.headerSortUpClass);
			    $(hdr).addClass(json.headerSortDownClass);

			} else {
			    $(hdr).addClass(json.headerSortUpClass);

			}

			var currentIndexPage = 0;
			if (!json.onOrderReturnToFirstPage) {
			    currentIndexPage = json.indexPage;
            }


			var params = "header=" + headerVal + "&order=" + dataOrder + "&indexPage=" + currentIndexPage;
			var sortingClass = hdr.className;

			showLoadingBar();

			$.ajax({
			    type: "GET",
			    url: json.urlAjax,
			    data: params,
			    dataType: "html",
			    cache: false,
			    statusCode: {
			        404: function () {
			            alert(json.ajaxNotFoundResource);
			        }
			    },
			    complete: function () {
			        hideLoadingBar();
			    },
			    success: function (data) {
			        if (data != null) {
			            var jqObj = jQuery(data);
			            jqObj.find("#" + json.tableId + " #" + headerVal).addClass(sortingClass);
			            $('.' + json.htmlElementAjaxResponseClass).html(jqObj);
			        }
			    },
			    error: function (erro) { }
			});

		}


	}

	$('th').click(function () {
	    if ($(this).attr("id")) {
		    handleHeaderClick(this, json);
		}

	});

	$("." + json.paginationClass).click(function () {
	    var link = $(this);
	    var params = "header=" + json.header + "&order=" + json.order;
	    var currentSortingClass = json.order == "ASC" ? "headerSortUp" : "headerSortDown";

	    showLoadingBar();

	    $.ajax({
	        url: link.attr('href'),
	        type: 'GET',
	        data: params,
	        cache: false,
	        success: function (data) {
	            if (data != null) {
	                var jqObj = jQuery(data);
	                jqObj.find("#" + json.tableId + " #" + json.header).addClass(currentSortingClass);
	                $('.' + json.htmlElementAjaxResponseClass).html(jqObj);
	            }
	        },
	        statusCode: {
	            404: function () {
	                alert(json.ajaxNotFoundResource);
	            }
	        },
	        complete: function () {
	            hideLoadingBar();
	        }
	    });

	    return false;

	});

}