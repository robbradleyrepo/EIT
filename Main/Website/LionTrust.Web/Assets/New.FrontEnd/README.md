#LionTrust Flats Starter

## Start project
for start project clone this repository and run ```npm i```

# Update 2023

FE Env has been updated to incorporate EIT/ project which sits parallel to app/ (LT as before)

Both projects work successfully in isolation from one another as reflected by isolated
gulp tasks, refactored and extended accordingly.

## Main Gulpfile.js options:

## LionTrust Project:

Unaffected by update, except for new gulp commands:

* ```gulp dev__LT``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```gulp build__LT```: project build


## Edinburgh Investment Trust Project:

* ```gulp dev__EIT``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```gulp build__EIT```: project build

## Destination dirs

* LT: ```dist/``` 
* EIT: ```dist/EIT/``` 


## Rules
* All scripts located in ```app/js/app.js | app.min.js```
* Main ```sass``` files located in ```app/styles/sass/styles.*```
* Each ```html component``` located in ```app/components```

##Include parts of HTML code:
 You can import any part of the code using construction in any of html files:

 `<!--#include virtual="/components/GC001-Header.html" -->`