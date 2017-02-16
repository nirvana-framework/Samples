"use strict";
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
var http_1 = require('@angular/http');
var app_routes_1 = require("./app.routes");
var app_component_1 = require('./app.component');
require("rxjs/add/operator/map");
var Common_1 = require("./models/CQRS/Common");
var channel_service_1 = require("./components/framework/signlar/channel.service");
var channelConfig = new Common_1.ChannelConfig();
channelConfig.url = "http://localhost:9123/signalr";
channelConfig.url = "http://local-uinotifications.mean.software:24171/signalr";
channelConfig.hubName = "EventHub";
platform_browser_dynamic_1.bootstrap(app_component_1.AppComponent, [
    http_1.HTTP_PROVIDERS,
    app_routes_1.APP_ROUTER_PROVIDERS,
    channel_service_1.ChannelService,
    { provide: Common_1.SignalrWindow, useValue: window, multi: false },
    { provide: "channel.config", useValue: channelConfig, multi: false }
]);
//# sourceMappingURL=main.js.map