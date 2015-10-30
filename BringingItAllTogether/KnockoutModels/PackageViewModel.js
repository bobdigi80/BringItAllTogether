/// <reference path="../Scripts/knockout-3.3.0.js" />
/// <reference path="../Scripts/jquery-1.10.2.js" />
var packageViewModel;

// use as register student views view model
function Package(id, title, description, location) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.Id = ko.observable(id);
    self.Title = ko.observable(title);
    self.Description = ko.observable(description);
    self.Location = ko.observable(location);
   
    self.addPackage = function () {
        var dataObject = ko.toJSON(this);
        
        $.ajax({
            url: '/api/package',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                packageViewModel.packageListViewModel.packages.push(new Package(data.Id, data.Title, data.Description, data.Location));

                self.Id(null);
                self.Title('');
                self.Description('');
                self.Location('');
            }
        });
    };
}

// use as student list view's view model
function PackageList() {
    var self = this;
    // observable arrays are update binding elements upon array changes
    self.packages = ko.observableArray([]);

    self.getPackages = function () {
        self.packages.removeAll();

        // retrieve students list from server side and push each object to model's students list
        $.getJSON('/api/package', function (data) {
            $.each(data, function (key, value) {
                self.packages.push(new Package(value.Id, value.Title, value.Description, value.Location));
            });
        });
    };

    // remove student. current data context object is passed to function automatically.
    self.removePackage = function (selectedPackage) {
        $.ajax({
            url: '/api/package/' + selectedPackage.Id(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.packages.remove(selectedPackage);
            }
        });
    };
}

// create index view view model which contain two models for partial views
packageViewModel = { addPackageViewModel: new Package(), packageListViewModel: new PackageList() };

// on document ready
$(document).ready(function () {
    // bind view model to referring view
    ko.applyBindings(packageViewModel);

    // load student data
    packageViewModel.packageListViewModel.getPackages();
});