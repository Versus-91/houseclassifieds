(function ($) {
    var _cityService = abp.services.app.report,
        l = abp.localization.getSource('classifieds'),
        _$modal = $('#ReportCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#ReportTable');

    var _$rolesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#RolesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _cityService.getAll(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        language: {
            "lengthMenu": "_MENU_ " + l("RecordPerPage"),
            "zeroRecords": "Nothing found - sorry",
            "infoEmpty": l("No records available"),
            "infoFiltered": "(filtered from _MAX_ total records)"
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
                type: 'column',
            }
        },
        columnDefs: [
            {
                className: 'control',
                defaultContent: '',
                orderable: false,
                targets: 0
            },
            {
                targets: 1,
                data: 'post.isVerified',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.post.isVerified === true ? `<span class="badge badge-success">فعال</span>` : `<span class="badge badge-danger">فیر فعال</span>`
                }
            },
            {
                targets: 2,
                data: 'post.title',
                sortable: false
            },
            {
                targets: 3,
                data: 'description',
                sortable: false
            },
            {
                targets: 4,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <a type="button" class="btn btn-sm bg-secondary " href="/admin/reports/show/${row.id}"  >`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Show')}`,
                        '   </a>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-district" data-district-id="${row.id}" data-report-name="${row.id}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var city = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _cityService
            .create(city)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$rolesTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-district', function () {
        var Id = $(this).attr("data-district-id");
        var Name = $(this).attr('data-district-name');

        deleteRole(Id, Name);
    });



    abp.event.on('district.edited', (data) => {
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
                    _cityService.delete({
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
