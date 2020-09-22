$(function () {
    var url = document.location.href;
    var params = url.split("?");
    var loading = false;
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
        loading = true;
        $.getJSON(path, function () {
        })
            .done(function (res) {
                $("#pager").empty();
                if (reloading) {
                    pageIndex = 1;
				}
                if (res.result.totalCount === 0) {
                    $("#pager").append(`<div class="column is-vcentered has-text-centered" style="min-height:500px;"><p class="subtitle has-text-centered">نتیجه ای یافت نشد .</p></div>`)
                }
                totalPages = Math.ceil(res.result.totalCount / rowPerPage);
                if (pageIndex <= 1) {
                    $(".pagination-previous").attr("disabled", "disabled");
                } else {
                    $(".pagination-previous").removeAttr("disabled");
                }
                if (pageIndex >= totalPages) {
                    $(".pagination-next").attr("disabled", "disabled");
                } else {
                    $(".pagination-next").removeAttr("disabled");
                }
                res.result.items.forEach((item) => {
                    var boolHasImage = item.images && item.images[0] ? true : false;
                    var hasImageIcon = boolHasImage == false ? `<span class="fa-stack ">
                                    <i class="fas fa-camera fa-stack-1x"></i>
                                    <i class="fas fa-ban fa-stack-2x" style="color:Tomato"></i>
                                    </span>` : `<span class="fa-stack " >
                                    <i class="fas fa-camera fa-stack-1x has-text-info" style=" vertical-align: middle;"></i><span>${item.images.length}</span></span>`;
                    var featuredIcon = item.isFeatured === true ? `<span class="fa-stack  has-text-success"><i class="fas fa-stack-1x fa-check-square"></i></span>` : ``;
                    $("#pager").append(`<div class=" column is-4">
                                <a class="fill-div" href="/ads/${item.id}">
                                    <div class="card card-hover-shadow">
                                        <div class="card-image">
                                            <figure class="image is-4by3">
                                                <img src="${boolHasImage ? item.images[0].path +'?width=600&height=480' : '/img/placeholder.png'}" alt="Placeholder image">
                                                <div class="overlay">${item.district.city.name + ',' + item.district.name}</div>
                                                <div class="overlay-top-corner">
                                                ${featuredIcon}
                                                ${hasImageIcon}
                                                </div>
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
                                                        <p>${item.age == 0 ? '--' : item.age}</p>
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
                loading = false;
                $('html, body').animate({
                    scrollTop: $("#mainContainer").offset().top - 20
                }, 200);
            })
            .fail(function () {
                console.log("error");
            })
            .always(function () {
                loading = false;
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
            query.minArea = minArea;
            query.maxArea = maxArea;
		} else {
            delete query.minArea ;
            delete query.maxArea ;
		}
        if (minPrice & maxPrice) {
            query.minPrice = minPrice;
            query.maxPrice = maxPrice ;
		} else {
            delete query.minPrice ;
            delete  query.maxPrice ;
		}
        if ($("#featured").is(":checked")) {
            query.featured = true;
        } else {
            delete query.featured;
        }
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
