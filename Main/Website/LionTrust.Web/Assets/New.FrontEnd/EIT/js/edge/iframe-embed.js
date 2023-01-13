"use strict"; 

export default () => {

	
    var childIFrame = $(".f-iframe-embed");
    Function.prototype.bind && console && typeof console.log == "object" && ["log", "info", "warn", "error", "assert", "dir", "clear", "profile", "profileEnd"].forEach(function(n) {
        console[n] = this.call(console[n], console)
    }, Function.prototype.bind);
    window.log || (window.log = function() {
        var n, t;
        if (log.history = log.history || [], log.history.push(arguments), typeof console != "undefined" && typeof console.log == "function")
            if (window.opera)
                for (n = 0; n < arguments.length;) console.log("Item " + (n + 1) + ": " + arguments[n]), n++;
            else Array.prototype.slice.call(arguments).length == 1 && typeof Array.prototype.slice.call(arguments)[0] == "string" ? console.log(Array.prototype.slice.call(arguments).toString()) : console.log(Array.prototype.slice.call(arguments));
        else Function.prototype.bind || typeof console == "undefined" || typeof console.log != "object" ? document.getElementById("firebug-lite") ? setTimeout(function() {
            log(Array.prototype.slice.call(arguments))
        }, 500) : (t = document.createElement("script"), t.type = "text/javascript", t.id = "firebug-lite", t.src = "https://getfirebug.com/firebug-lite.js", document.getElementsByTagName("HEAD")[0].appendChild(t), setTimeout(function() {
            log(Array.prototype.slice.call(arguments))
        }, 2e3)) : Function.prototype.call.call(console.log, console, Array.prototype.slice.call(arguments))
    });
    typeof $ == "undefined" && console.log("**jQuery and the 'pm' plugin are both required for postmessaging**");
    typeof pm == "undefined" && console.log("**jQuery and the 'pm' plugin are both required for postmessaging**");
    var isChildFrameLoaded = !1,
        isDocumentReady = !1,
        hasParentUrlBeenSent = !1,
        pmOptions = {};
    pmOptions.SendParentUrlOptions = {
        type: "ParentURL",
        target: childIFrame.contentWindow,
        url: childIFrame.attr("src"),
        success: function(n) {
            SuccessfulSend(n)
        },
        error: function(n) {
            FailedSend(n)
        },
        data: {
            ParentUrl: window.location.href
        }
    };
    pm.bind("IFrameSize", function(n) {
        ReceivedIFrameSize(n)
    }, GetDomain(childIFrame.attr("src")));
    pm.bind("IFrameFunds", function() {}, GetDomain(childIFrame.attr("src")));
    childIFrame.one("load", function() {
        DebugAlert("PARENT:one.load() #childFrame loaded");
        DebugAlert("PARENT:one.load() child domain-" + GetDomain(childIFrame.attr("src")));
        isDocumentReady && !hasParentUrlBeenSent && (DebugAlert("PARENT:one.load() document.ready=true and parentUrl has not already been sent."), hasParentUrlBeenSent = !0, SendParentUrl());
        isChildFrameLoaded = !0
    });
    $(document).ready(function() {
        isDocumentReady = !0;
        DebugAlert("PARENT:document.ready()");
        isChildFrameLoaded && !hasParentUrlBeenSent && (DebugAlert("PARENT:document.ready() and iframe already loaded"), hasParentUrlBeenSent = !0, SendParentUrl())
    })

}