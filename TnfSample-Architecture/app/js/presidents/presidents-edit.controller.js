/**
* @license TOTVS | Totvs TNF THF v0.1.0
* (c) 2015-2016 TOTVS S/A https://www.totvs.com
* License: Comercial
*/

/**
* @module totvsPresident
* @name TotvsPresidentListController
* @object controller
*
* @created 2017-3-6 v0.1.0
* @updated 2017-3-6 v0.1.0
*
* @requires
*
* @dependencies
*
* @description Controller responsável por editar o registro
*/

(function () {

    'use strict';

    angular
        .module('president')
        .controller('PresidentEditController', PresidentEditController);

    PresidentEditController.$inject = [
        '$scope',
        '$stateParams',
        '$state',
        '$window',
        'totvs.app-notification.Service',
        'i18nFilter',
		'presidentFactory'
    ];

	function PresidentEditController(
        $scope, $stateParams, $state, $window, notification, i18nFilter, presidentFactory) {

		// *********************************************************************************
		// *** Variables
		// *********************************************************************************

		var self = this;

        // *********************************************************************************
		// *** Public Properties and Methods
		// *********************************************************************************

        self.president = {};
        self.cancel = cancel;
        self.save = save;
        self.saveNew = saveNew;

        // *********************************************************************************
		// *** Controller Initialize
		// *********************************************************************************

        function init(cacheController) {

            if (!cacheController) {
                if ($stateParams && $stateParams.id) {
                    loadRecord($stateParams.id);
                }
            } else {
                // Buscando dados iniciais do "cache"
                angular.forEach(cacheController, function(value, property) {
                    self[property] = value;
                });
            }

		}

        // *********************************************************************************
		// *** Events Listners
		// *********************************************************************************

        $scope.$on('$totvsViewServiceInit', function (event, cacheController) {
            init(cacheController);
        });

		// *********************************************************************************
		// *** Functions
		// *********************************************************************************

		function loadRecord(id) {
			presidentFactory.getRecord(id, function (president) {
				if (president){
                    if(president.id)
					    self.president = president;
                    if(president.data && president.data.id)
					    self.president = president.data;
				} else {
                    notification.notify({
                        type: 'warning',
                        title: '404',
                        detail: 'Registro "' + id + '" não encontrado, mas você pode inserir um novo registro. =P'
                    });

                    $state.go('presidents.new');
                }
			});
		}

        function cancel() {
			notification.question({
				title: 'l-question',
				text: i18nFilter('l-cancel-operation'),
				cancelLabel: 'l-no',
				confirmLabel: 'l-yes',
				callback: function (isPositiveResult) {
					if (isPositiveResult) {
						$window.history.back();
					}
				}
			});
		}

        function save() {
			if (self.president.id) {
				presidentFactory.updateRecord(self.president.id, self.president, function (result) {
					$state.go('presidents.detail', {id: self.president.id});
				});
			} else {
				presidentFactory.saveRecord([self.president], function (result) {
					$state.go('presidents.start');
				});
			}
		}

        function saveNew() {
			if (self.president.id) {
				presidentFactory.updateRecord(self.president.id, self.president, function (result) {
					$state.go('president.new');
				});
			} else {
				presidentFactory.saveRecord([self.president], function (result) {
					setTimeout(function() {
                    	$state.go($state.current, {}, {reload: true});
					}, 5000);
				});
			}
		}
	}
}());