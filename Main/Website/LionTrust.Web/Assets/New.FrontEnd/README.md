#LionTrust Flats Starter

## Start project
for start project clone this repository and run ```npm i```

## Main Gulpfile.js options:

* ```gulp``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```build```: project build

## Rules
* All scripts located in ```app/js/app.js | app.min.js```
* Main ```sass``` files located in ```app/styles/sass/styles.*```
* Each ```html component``` located in ```app/components```

##Include parts of HTML code:
 You can import any part of the code using construction in any of html files:

 `<!--#include virtual="/components/GC001-Header.html" -->`