$(function () {
    var url = document.location.href;
    var params = url.split("?");
    console.log(params);
    var pageIndex = 1;
    var rowPerPage = 30;
    var totalPages = 0;
    var path = function (filter) {
        console.log('filter : ',filter)
		if (filter) {
            return '/api/services/app/post/getall?SkipCount=' + (pageIndex - 1) * rowPerPage + '&MaxResultCount=' + rowPerPage +'&'+ filter;
		}
        return '/api/services/app/post/getall?SkipCount=' + (pageIndex - 1) * rowPerPage + '&MaxResultCount=' + rowPerPage ;
    }
    function loadAds(path, reloading = false) {
        $.getJSON(path, function () {
        })
            .done(function (res) {
                $("#pager").empty();
                if (reloading) {
                    pageIndex = 1;
				}
                if (res.result.totalCount === 0) {
                    $("#pager").append(`<div class="column is-vcentered has-text-centered"><p class="subtitle has-text-centered">نتیجه ای یافت نشد .</p></div>`)
                }
                totalPages = Math.ceil(res.result.totalCount / rowPerPage);
                if (pageIndex <= 1) {
                    $(".pagination-previous").attr("disabled", "disabled");
                } else {
                    console.log("enable prev");
                    $(".pagination-previous").removeAttr("disabled");
                }
                if (pageIndex >= totalPages) {
                    $(".pagination-next").attr("disabled", "disabled");
                } else {
                    $(".pagination-next").removeAttr("disabled");
                }

                res.result.items.forEach((item) => {
                    var featuredIcon = item.isFeatured === true ? `<i class="fas  fa-check has-text-success"></i>` : `<i class="fas fa-times has-text-danger"></i>`;
                    $("#pager").append(`<div class=" column is-4">
                                <a class="fill-div" href="/ads/${item.id}">
                                    <div class="card card-hover-shadow">
                                        <div class="card-image">
                                            <figure class="image is-4by3">
                                                <img src="https://images.unsplash.com/photo-1580587771525-78b9dba3b914?ixlib=rb-1.2.1&w=1000&q=80" alt="Placeholder image">
                                                <div class="overlay">${new persianDate(item.creationTime)}</div>


                                                <div class="overlay-top-corner">${featuredIcon}</div>
                                            </figure>
                                        </div>
                                        <div class="card-content">
                                            <div class="content">
                                                ${item.title}
                                                <br>
                                            </div>
                                            <nav class="level is-mobile">
                                                <div class="level-item has-text-centered">
                                                    <div>
                                                        <p><i class="fas  fa-square has-text-success"></i></p>
                                                        <p>${item.area}</p>
                                                    </div>
                                                </div>
                                                <div class="level-item has-text-centered">
                                                    <div>
                                                        <p><i class="fas  fa-clock has-text-warning"></i></p>
                                                        <p>نوساز</p>
                                                    </div>
                                                </div>
                                                <div class="level-item has-text-centered">
                                                    <div>
                                                        <p><i class="fas fa-bed has-text-grey"></i></p>
                                                        <p>${item.bedroom}</p>
                                                    </div>
                                                </div>
                                            </nav>
                                        </div>

                                    </div>
                                </a>
                            </div>`);
                });
                $('html, body').animate({
                    scrollTop: $("#mainContainer").offset().top
                }, 200);
            })
            .fail(function () {
                console.log("error");
            })
            .always(function () {
                console.log("complete");
            });
    }
    function FilterAds() {
        var query = {};
        var minArea = $("#minArea").val();
        var maxArea = $("#maxArea").val();
        var minPrice = $("#minPrice").val();
        var maxPrice = $("#maxPrice").val();
        if (minArea & maxArea) {
            console.log('area');
            query.minArea = minArea;
            query.maxArea = maxArea;
		} else {
            delete query.minArea ;
            delete query.maxArea ;
		}
        if (minPrice & maxPrice) {
            delete query.minPrice ;
            delete query.maxPrice ;
		} else {
            query.minPrice ;
            query.maxPrice ;
		}
        if ($("#featured").is(":checked")) {
            query.featured = true;
        } else {
            delete query.featured;
        }
        console.log(query);
        loadAds(path($.param(query)),true);
    }
    $("#apply").click(() => {
        FilterAds();
    });
    $(".pagination-next").click(() => {
        pageIndex= pageIndex + 1;
        loadAds(path())
    });
    $(".pagination-previous").click(() => {
        pageIndex = pageIndex  - 1;
        loadAds(path())
    });
    loadAds(path(params[1]))
})
