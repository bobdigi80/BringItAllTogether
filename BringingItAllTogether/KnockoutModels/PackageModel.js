function packagemodel(id, title) {
    return {
        id: ko.observable(id),
        title: ko.observable(title),
        description: ko.observable(description),
        location: ko.observable(location),
        isEdit: ko.observable(false)
    };
}