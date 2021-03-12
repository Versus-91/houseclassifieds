(function ($) {
    var DateDiff = {

        inDays: function (d1, d2) {
            var t2 = d2.getTime();
            var t1 = d1.getTime();

            return parseInt((t2 - t1) / (24 * 3600 * 1000));
        },

        inWeeks: function (d1, d2) {
            var t2 = d2.getTime();
            var t1 = d1.getTime();

            return parseInt((t2 - t1) / (24 * 3600 * 1000 * 7));
        },

        inMonths: function (d1, d2) {
            var d1Y = d1.getFullYear();
            var d2Y = d2.getFullYear();
            var d1M = d1.getMonth();
            var d2M = d2.getMonth();

            return (d2M + 12 * d2Y) - (d1M + 12 * d1Y);
        },

        inYears: function (d1, d2) {
            return d2.getFullYear() - d1.getFullYear();
        }
    }
    var _postService = abp.services.app.adminPost,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#PostCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#PostsTable');
    var _$rolesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _postService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$rolesTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'category',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.category.name + " , "+ row.type.name;
                }
            },
            {
                targets: 2,
                data: 'district',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.district.area.city.name + ',' + row.district.name +" , " + row.district.area.name;
                }
            },
            {
                targets: 3,
                data: 'isFeatured',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.isFeatured === true ? `<span class="badge badge-success">فوری</span>` :`<span class="badge badge-danger">عادی</span>`
                }
            },
            {
                targets: 4,
                data: 'isVerified',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.isVerified === true ? `<span class="badge badge-success">تایید شده</span>` : `<span class="badge badge-danger">تایید نشده</span>`
                }
            },
            {
                targets: 5,
                data: 'creationTime',
                sortable: false,
                render: (data, type, row, meta) => {
                    var days = DateDiff.inDays(new Date(row.creationTime), new Date());
					if (days <=1 ) {
                        return "امروز";
					}
                    return days + " روز قبل"  ;
                }
            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <a type="button" class="btn btn-sm bg-secondary " href="/admin/posts/${row.id}">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Show')}`,
                        '   </a>',
                        //`   <button type="button" class="btn btn-sm bg-danger delete-post" data-post-id="${row.id}" data-post-name="${row.title}">`,
                        //`       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        //'   </button>',
                    ].join('');
                }
            }
        ]
    });


    $(document).on('click', '.delete-post', function () {
        var id = $(this).attr("data-post-id");
        var name = $(this).attr('data-post-name');

        deleteRole(id, name);
    });

 

    abp.event.on('post.edited', (data) => {
        _$rolesTable.ajax.reload();
    });

    function deleteRole(cityId, cityName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                cityName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _postService.delete({
                        id: cityId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$rolesTable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$rolesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$rolesTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
