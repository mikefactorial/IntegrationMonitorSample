 /// <reference path="../../JScript/SDK.REST.js" />
function IntegrationMonitorModel() {
    var self = this;
    self.pingResults = ko.observableArray([]);

    //  methods/functions
    self.callAction = function (action, data, callback, errHandler) {
        var serverURL = Xrm.Page.context.getClientUrl();
        var req = new XMLHttpRequest();
        // specify name of the entity, record id and name of the action in the Wen API Url
        req.open("POST", serverURL + "/api/data/v9.0/" + action, true);
        req.setRequestHeader("Accept", "application/json");
        req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
        req.setRequestHeader("OData-MaxVersion", "4.0");
        req.setRequestHeader("OData-Version", "4.0");
        req.onreadystatechange = function () {
            if (this.readyState == 4 /* complete */) {
                req.onreadystatechange = null;
                if (this.status == 200 && this.response != null) {
                    var data = JSON.parse(this.response);
                    callback(data);
                }
                else {
                    if (this.response != null && this.response != "") {
                        var error = JSON.parse(this.response).error;
                        errHandler(error.message);
                    }
                }
            }
        };
        // send the request with the data for the input parameter
        req.send(window.JSON.stringify(data));
    }

    self.callAction('mf_PingIntegrations', { "Request": "testing" },
        function (data) {
            self.pingResults.removeAll();
            //console.log(data);
            if (data.Response.length == 0) {
                //  no records returned
            }
            else {
                var responses = JSON.parse(data.Response);
                if (responses.length > 0) {
                    ko.utils.arrayForEach(responses, function (d) {
                        self.pingResults.push(d);
                    });
                };
            }
        },
        function (err) { }
    );
};
