$(function () {

    $('.select2').select2({
        matcher: matchCustom,
        language: {
            "noResults": function () { return 'موردی پیدا نشد.'; }
        }
    });
    $('#selectCity').on('select2:select', function (e) {
        if (e.target.value != 0) {
            $("#selectDistrict").prop("disabled", false);
            var sampleArray = [{ id: 0, text: 'enhancement' }, { id: 1, text: 'bug' }
                , { id: 2, text: 'duplicate' }, { id: 3, text: 'invalid' }
                , { id: 4, text: 'wontfix' }];
            $.get('/admin/districts/GetByCityId?id=' + e.target.value, function (res) {
                var items = res.result.map(m => {
                    return {
                        id: m.id,
                        text: m.name
                    }
                }
                );
                items.splice(0, 0, { id: 0, text: "انتخاب محله" })
                $("#selectDistrict").empty();
                $("#selectDistrict").select2({ data: items });
            })
        } else {
            $("#selectDistrict").prop("disabled", true);
        }
    });
    function matchCustom(params, data) {
        // If there are no search terms, return all of the data
        if ($.trim(params.term) === '') {
            return data;
        }

        // Do not display the item if there is no 'text' property
        if (typeof data.text === 'undefined') {
            return null;
        }

        // `params.term` should be the term that is used for searching
        // `data.text` is the text that is displayed for the data object
        if (data.text.indexOf(params.term) > -1) {
            var modifiedData = $.extend({}, data, true);
            // You can return modified objects from here
            // This includes matching the `children` how you want in nested data sets
            return modifiedData;
        }

        // Return `null` if the term should not be displayed
        return null;
    }
});
Vue.directive('select2', {
    inserted(el) {
        $(el).on('select2:select', () => {
            const event = new Event('change', { bubbles: true, cancelable: true });
            el.dispatchEvent(event);
        });

        $(el).on('select2:unselect', () => {
            const event = new Event('change', { bubbles: true, cancelable: true })
            el.dispatchEvent(event)
        })
    },
});
new Vue({
    el: "#app",
    data: {
        district: 0,
        loading: false,
        total:null,
        city: 0,
        items: []
    },
    methods: {
        search: function () {
            this.loading = true;
            var query = [];
            let url = "/api/services/app/realestate/getall";
            if (!!this.city && this.city !=0) {
                query.push('city=' + this.city);
            }
            if (!!this.district && this.district != 0) {
                query.push('district=' + this.district);
            }
            if (query.length>0) {
                url += '?'+ query.join('&');
            }
            axios.get(url).then((res) => {
                this.loading = false;
                this.total = res.data.result.totalCount;
                this.items = res.data.result.items;
            });
        }
    }
});