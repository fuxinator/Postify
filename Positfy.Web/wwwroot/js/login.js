function LoginViewModel() {
    var self = this;
    self.Username = ko.observable();
    self.Password = ko.observable();
    self.Message = ko.observable();
    self.Class = ko.observable();

    self.LogIn = function() {
        $.ajax({
            type: "POST",
            url: "LoginUser",
            data: { "Username": self.Username(), "Password": self.Password() },
            dataType: "json",
            success: function(response) {
                if (response !== null) {
                  self.Message(response.message);
                  if (response.type === "success") {
                    window.location = response.url;
                  } else {
                    self.Class("alert alert-danger");
                  }
                }
            },
            failure: function(response) {
                alert("Error while retrieving data!");
            }
        });
    };
}

ko.applyBindings(new LoginViewModel());
