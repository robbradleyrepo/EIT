## Setup once cloned repo to machine
* Within *CMD/powershell*, navigate to: $ProjectSource$\Website\liontrustfrontend\Main\Website\LionTrust.Web\Assets\New.FrontEnd
** For first time setup, run "npm-install"
* Run "gulp"

This should automatically open a new browser window with the Liontrust site using a localhost port. 

The gulp task builds the site and also runs a browsersync and watch task which will refresh the browser after any change to the HTML,JS or SASS.

To just build the site and deploy into the dist($ProjectSource$\Website\liontrustfrontend\Main\Website\LionTrust.Web\Assets\New.FrontEnd\dist) folder without starting the site, run "gulp build".

In your IDE, the root of the code for front end lives here: $ProjectSource$\Website\liontrustfrontend\Main\Website\LionTrust.Web\Assets\New.FrontEnd\app

To view different pages is simply looking in the root HTML files and navigating to them in browser, for example: http://localhost:3000/fund-detail.html



HTML Updates:
A components HTML lives here:  $ProjectSource$\Website\liontrustfrontend\Main\Website\LionTrust.Web\Assets\New.FrontEnd\app\components.
Please note: With any HTML, the front end dev must notify the backend dev of the change.

SASS Updates:
$ProjectSource$\Website\liontrustfrontend\Main\Website\LionTrust.Web\Assets\New.FrontEnd\app\styles\sass

js:
$ProjectSource$\Website\liontrustfrontend\Main\Website\LionTrust.Web\Assets\New.FrontEnd\app\js