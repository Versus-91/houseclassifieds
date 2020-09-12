(function ($) {
    var _postService = abp.services.app.adminPost,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#PostCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#PostsTable');
    console.log(abp.services.app);
    var _$rolesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _postService.getAllDetails(filter).done(function (result) {
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
                data: 'title',
                sortable: false
            },
            {
                targets: 1,
                data: 'category',
                sortable: false
            },
            {
                targets: 2,
                data: 'district',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.city + ',' + row.district;
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
                    console.log(row)
                    return row.isVerified === true ? `<span class="badge badge-success">تایید شده</span>` : `<span class="badge badge-danger">تایید نشده</span>`
                }
            },
            {
                targets: 5,
                data: 'creationTime',
                sortable: false,
                render: (data, type, row, meta) => {
                    return new Date(row.creationTime);
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
