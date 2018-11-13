function RegisterViewModel() {
    var self = this;
    self.Username = ko.observable();
    self.Email = ko.observable("");
    self.Password = ko.observable("");
    self.ConfirmPassword = ko.observable("");
    self.Message = ko.observable();
    self.Class = ko.observable();

    self.Register = function() {
        $.ajax({
            type: "POST",
            url: "RegisterUser",
            data: { "Username": self.Username(), "Email": self.Email(), "Password": self.Password(), "ConfirmPassword": self.ConfirmPassword() },
            dataType: "json",
            success: function(response) {
                if (response !== null) {
                  self.Message(response.description);
                  self.Class(response.class);
                }
            },
            failure: function(response) {
                alert("Error while retrieving data!");
            }
        });
    };
}

ko.applyBindings(new RegisterViewModel());
