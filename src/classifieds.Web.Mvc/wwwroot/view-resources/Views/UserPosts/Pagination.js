$(function () {
    $('.select2').select2();
    var url = document.location.href;
    var params = url.split("?");
    var loading = false;
    var pageIndex = 1;
    var rowPerPage = 30;
    var totalPages = 0
    $('.tabs').each(function (index) {
        var $tabParent = $(this);
        var $tabs = $tabParent.find('li');
        var $contents = $tabParent.next('.tabs-content').find('.tab-content');

        $tabs.click(function () {
            var curIndex = $(this).index();
            // toggle tabs
            $tabs.removeClass('is-active');
            $tabs.eq(curIndex).addClass('is-active');
            // toggle contents
            $contents.removeClass('is-active');
            $contents.eq(curIndex).addClass('is-active');
        });
    });

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
                    $("#pager").append(`<div class="column is-vcentered has-text-centered" style="min-height:350px;"><p class="subtitle has-text-centered">نتیجه ای یافت نشد .</p></div>`)
                }
                totalPages = Math.ceil(res.result.totalCount / rowPerPage);
                if (pageIndex <= 1) {
                    $(".pagination-previous").attr("disabled", true);
                } else {
                    $(".pagination-previous").removeAttr("disabled");
                }
                if (pageIndex >= totalPages) {
                    $(".pagination-next").attr("disabled", true);
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
                    var priceTag = item.category.name.includes("گروی") || item.category.name.includes("اجاره") ? `اجاره : ${item.rent}<br>پیش پرداخت : ${item.deposit} ` : `${item.price}`;
                
                    $("#pager").append(`<div class=" column is-12-mobile is-half-tablet is-one-third-desktop  is-one-third-fullhd">
                                <a class="fill-div has-text-grey" href="/ads/${item.id}">
                                    <div class="card card-hover-shadow" data-label="${item.category.name}">
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
                                            <div class="content has-text-centered">
                                                ${priceTag}
                                            </div>
                                            <nav class="level is-mobile">
                                                <div class="level-item has-text-centered">
                                                    <div>
                                                        <p><i class="fas fa-chart-area has-text-grey"></i></p>
                                                        <p>${item.area}</p>
                                                    </div>
                                                </div>
                                                <div class="level-item has-text-centered">
                                                    <div>
                                                        <p><i class="fas  fa-clock has-text-grey"></i></p>
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
                                        <footer class="card-footer">
                                          <a href="#" class="card-footer-item">
                                              <p><i class="fas fa-phone has-text-success"></i>  اطلاعات تماس</p>
                                          </a>
                                        </footer>
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
                loading = false;
                console.log("error");
            })
            .always(function () {
                loading = false;
                console.log("complete");
            });
    }
    var selectedCategory = $('#categories').find('option:selected').text();
    if (selectedCategory.includes("اجاره") || selectedCategory.includes("گروی")) {
        $("#sale").hide();
        $("#rent").show();
    } else {
        $("#sale").show();
        $("#rent").hide();
    }
    $('#categories').on('change', function () {
        var label = $(this).find('option:selected').text();
        if (label.includes("اجاره") || label.includes("گروی")) {
            $("#sale").hide();
            $("#rent").show();
            $("#minPrice").val(null);
            $("#maxPrice").val(null);
		} else {
            $("#sale").show();
            $("#rent").hide();
            $("#minRent").val(null);
            $("#maxRent").val(null);
            $("#minDeposit").val(null);
            $("#maxDeposit").val(null);
		}
    }); 
    function FilterAds(sorting = null) {
        var query = {};
        var category = $('#categories').find(":selected").val();
        var selectedCategory = $('#categories').find('option:selected').text();
        var selectedDistrict = $('#selectDistrict').find('option:selected').val();
        var selectedCity = $('#selectCity').find('option:selected').val();
        var peopertTypes = $('#propertyTypes').val();
        var minArea = $("#minArea").val();
        var maxArea = $("#maxArea").val();
        var minPrice = $("#minPrice").val();
        var maxPrice = $("#maxPrice").val();
        var minRent = $("#minRent").val();
        var maxRent = $("#maxRent").val();
        var minDeposit = $("#minDeposit").val();
        var maxDeposit = $("#maxDeposit").val();
        var beds = $("#minBedCount").val();
        if (!!selectedDistrict) {
            if (selectedDistrict != 0) {
                query.district = selectedDistrict;
			}
        }
        //if (!!selectedCity) {
        //    if (selectedCity != 0) {
        //        query.city = selectedCity;
        //    }
        //}
        if (!!category) {
            query.category = category;
            if (selectedCategory.includes("اجاره") || selectedCategory.includes("گروی")) {
                if (minRent & maxRent) {
                    query.minRent = minRent;
                    query.maxRent = maxRent;
                } else {
                    delete query.minRent;
                    delete query.maxRent;
                }
                 if (minDeposit & maxDeposit) {
                    query.minDeposit = minDeposit;
                    query.maxDeposit = maxDeposit;
                } else {
                    delete query.minDeposit;
                    delete query.maxDeposit;
                }
            } else {
                if (minPrice & maxPrice) {
                    query.minPrice = minPrice;
                    query.maxPrice = maxPrice;
                } else {
                    delete query.minPrice;
                    delete query.maxPrice;
                }
            }
        }
        if (!!peopertTypes) {
            if (peopertTypes != 0) { query.types = peopertTypes; }

        }
        if (minArea & maxArea) {
            query.minArea = minArea;
            query.maxArea = maxArea;
		} else {
            delete query.minArea ;
            delete query.maxArea ;
		}

        if ($("#featured").is(":checked")) {
            query.featured = true;
        } else {
            delete query.featured;
        }
        if (!!beds) {
            query.beds = beds;
        } else {
            delete query.beds;
        }
        if (sorting) {
            query.sorting = sorting;
		}
        loadAds(path($.param(query,true),true));
    }
    $("#apply").click(() => {
        FilterAds();
    });
    $("#send").click(() => {
        FilterAds();
    });
    $("#cheapest").click(() => {
        var category = $('#categories').find(":selected").val();
        var query = "";
        if (!!category) {
            if (selectedCategory.includes("اجاره") || selectedCategory.includes("گروی")) {
                query = "Rent,Deposit Asc"
            } else {
                query = "Price Asc"
            }
        }
        FilterAds(query);
    });
    $("#newest").click(() => {
        var query = "CreationTime Desc";
        FilterAds(query);
    });
    $("#mostExpensive").click(() => {
        var category = $('#categories').find(":selected").val();
        var query = "";
        if (!!category) {
            if (selectedCategory.includes("اجاره") || selectedCategory.includes("گروی")) {
                query = "Rent,Deposit Desc"
            } else {
                query = "Price Desc"
            }
        }
        FilterAds(query);
    });
    $(".pagination-next").click(() => {
            pageIndex = pageIndex + 1;
            loadAds(path());
    });
    $(".pagination-previous").click(() => {
        if ($(this).attr('disabled')) {
            return;
        }
        pageIndex = pageIndex  - 1;
        loadAds(path());
    });
    loadAds(path(params[1]))
})
