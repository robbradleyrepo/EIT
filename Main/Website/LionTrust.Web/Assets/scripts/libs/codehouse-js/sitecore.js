/* 
 * ---------------------------------------- *
 * Name: 	Sitecore                        *
 * Type: 	JavaScript Class                *
 * Version: v2.0.0                          *
 * Author:	Matt O'Neill                    *
 * Status:	Release                         *
 * ---------------------------------------- *
 */

var chSc = function () { var e = false, t = false, n = false, r = function () { try { if (window.Sitecore == undefined) { n = true; return } if (window.Sitecore.PageModes !== undefined) { e = true } else { t = true } } catch (r) { } }(); return { edit: e, preview: t, live: n } }()
