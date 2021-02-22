/////////////////////////////////////////
/// We first need a reference to the id of the child IFrame
var childIFrame = $('#childFrame');
//////////////////////////////////////

////////////////////////////////////////
// Cross-Browser Console Output
////////////////////////////////////////
if (Function.prototype.bind && console && typeof console.log == "object") { ["log", "info", "warn", "error", "assert", "dir", "clear", "profile", "profileEnd"].forEach(

function (method) { 

console[method] = this.call(console[method], console); 
}, 
Function.prototype.bind); 
}

if (!window.log) {
    window.log = function () {
        log.history = log.history || []; log.history.push(arguments); if (typeof console != 'undefined' && typeof console.log == 'function') {
            if (window.opera) { var i = 0; while (i < arguments.length) { console.log("Item " + (i + 1) + ": " + arguments[i]); i++; } }
            else if ((Array.prototype.slice.call(arguments)).length == 1 && typeof Array.prototype.slice.call(arguments)[0] == 'string') { console.log((Array.prototype.slice.call(arguments)).toString()); }
            else { console.log(Array.prototype.slice.call(arguments)); }
        }
        else if (!Function.prototype.bind && typeof console != 'undefined' && typeof console.log == 'object') { Function.prototype.call.call(console.log, console, Array.prototype.slice.call(arguments)); }
        else {
            if (!document.getElementById('firebug-lite')) { var script = document.createElement('script'); script.type = "text/javascript"; script.id = 'firebug-lite'; script.src = 'https://getfirebug.com/firebug-lite.js'; document.getElementsByTagName('HEAD')[0].appendChild(script); setTimeout(function () { log(Array.prototype.slice.call(arguments)); }, 2000); }
            else { setTimeout(function () { log(Array.prototype.slice.call(arguments)); }, 500); }
        }
    }
}

///////////////////
// Resource Check
///////////////////
if (typeof ($) == 'undefined') {
    console.log("**jQuery and the 'pm' plugin are both required for postmessaging**");
}
if (typeof (pm) == 'undefined') {
    console.log("**jQuery and the 'pm' plugin are both required for postmessaging**");
}

function DebugAlert(msg){
	var now = new Date();
	now = "" + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds() + ":" + now.getMilliseconds();
	console.log(msg);
}


// Send the parent's URL to the child iframe when it has loaded
// The 'one' method fires only once (as the name suggests)
var isChildFrameLoaded = false;
var isDocumentReady = false;
var hasParentUrlBeenSent = false;

/////////////////////////////////////////////////////////////////////////////////////////////////
// Send the parent URL to the child, so it can use it in the hash-hack (legacy) XSS method.
/////////////////////////////////////////////////////////////////////////////////////////////////	
// Setup Parent-to-child PM options
var pmOptions = {};

pmOptions.SendParentUrlOptions = {
	// Target Configuration
	type : "ParentURL",	// Conversation 'Type'
	target : childIFrame.contentWindow,
	url : childIFrame.attr("src"),
	// Callback methods		
	success : function(data){SuccessfulSend(data);},
	error : function(data){FailedSend(data);},
	// Data to send (JSON object)
	data : {"ParentUrl" : window.location.href }		
};

pm.bind(
	"IFrameSize", 
	function(data){
		ReceivedIFrameSize(data); 
	},
	GetDomain(childIFrame.attr("src")) 
);

pm.bind(
	"IFrameFunds", 
	function(data){
		// The eventHandler for fund export. Data now contains the list of funds exported from toolkit. The client is now able to react according to their needs.

	},
	GetDomain(childIFrame.attr("src")) 
);

function SendParentUrl(){
	pm(pmOptions.SendParentUrlOptions);		
}

childIFrame.one('load', function() {
	DebugAlert('PARENT:one.load() #childFrame loaded');
	DebugAlert('PARENT:one.load() child domain-' + GetDomain(childIFrame.attr("src")));
	// FOR IE8 (which doesn't load the iframe when rendering on 'back' button).
	if (isDocumentReady && !hasParentUrlBeenSent){
		DebugAlert('PARENT:one.load() document.ready=true and parentUrl has not already been sent.');
		hasParentUrlBeenSent = true;
		SendParentUrl();
	}
	isChildFrameLoaded = true;
});

$(document).ready(function(){

	isDocumentReady = true;
	//////////////////////////////////////////////////
	// Open a LISTENER with the CHILD domain.
	// NOTE: This will vary between staging and live.
	//////////////////////////////////////////////////
	DebugAlert('PARENT:document.ready()');

	// For IE8:
	if (isChildFrameLoaded && !hasParentUrlBeenSent){
	DebugAlert('PARENT:document.ready() and iframe already loaded');
		 //Either the child frame's onload event will fire after the document 
		 //is ready, which will cause the resize method to run OR this will run:
		hasParentUrlBeenSent = true;
		SendParentUrl();	
	}
});

/////////////////////////
// The Listen Functions
/////////////////////////
function ReceivedIFrameSize(data){
		childIFrame.height(data.Height).width(data.Width);
}

function GetDomain(url) {
	DebugAlert('PARENT:GetDomain()');
	if (url.indexOf("http") < 0){
		url = window.location.protocol + url;
	}
	var dom = window.location.protocol + "//" + url.match(/:\/\/(.[^/]+)/)[1];
	return dom;
}

/////////////////////////////////////////
// Methods to handle PM success/failure
/////////////////////////////////////////
function SuccessfulSend(data){
	// This is called when a message is sent successully
	DebugAlert('PARENT: successful send');
}

function FailedSend(data){
	// This is called if a message fails to be sent
	DebugAlert('PARENT: failed send');
}


