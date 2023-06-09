# LionTrust & Edinburgh Investment Trust FE

Update 2023:

FE Env has been updated to incorporate EIT/ project for Edinburgh Investment Trust site

Both projects work successfully in isolation from one another in DEVELOPMENT mode. 

BUILD task will build BOTH projects into /dist/**/

## Installation

Install node.js

Install nvm - https://github.com/nvm-sh/nvm

Run ```nvm install 12.9.1```

Run ```nvm use 12.9.1```

Run ```npm i```
 

## LionTrust Project: 

* ```gulp lt``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```gulp build```: project build


## Edinburgh Investment Trust Project:

* ```gulp eit``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```gulp build```: project build

```gulp build``` command will execute BOTH projects concurrently.


## Destination dirs

* LT: ```dist/LT/``` 
* EIT: ```dist/EIT/``` 


## Declaring asset paths

* HTML: ```<img src="images/***" />```
* SASS: ```background-image: url("../images/***")```



# EIT Specific

Bootstrap has been stripped out. 

New Breakpoint, Spacing and Grid system now work inconjunction, allowing nested mixins to play nice.

## Style Guide

```index.html``` for WIP primitive SG list of components

To do: https://github.com/mpetrovich/stylemark

## Grid

GridilyDidily

```EIT\styles\sass\vendor\gridilydidily.scss```

Usage - http://philippkuehn.github.io/gridilydidily/

## Spacings

Sass Spacing

```EIT\styles\sass\vendor\sass-spacing.scss```

Usage (SASS) - https://github.com/digitaledgeit/sass-spacing

## Breakpoints

```EIT\styles\sass\core\_breakpoints.sass```

Usage (SASS) min width mixins

* ``` @include sm ```
* ``` @include md ```
* ``` @include lg ```
* ``` @include xl ```
* ``` @include xxl ```

$screen-sm-min: 576px
$screen-md-min: 768px
$screen-lg-min: 992px
$screen-xl-min: 1200px
$screen-xxl-min: 1440px


## BEMIT 

Class prefixes in play:

* ```t-***``` - Template
* ```u-***``` - Utility (Containers, Site Wrappers, Generic)
* ```g-***``` - Component (Global)
* ```c-***``` - Component (Local)
* ```e-***``` - Element
* ```f-***``` - Function - purely an additional class if JS required. Components should not be styled using this.   

More info - https://gist.github.com/stephenway/a6145d9b4430e8c55a77

## Rules

* All scripts located in ```app/js/app.js | app.min.js```
* Main ```sass``` files located in ```app/styles/sass/styles.*```
* Each ```html component``` located in ```app/components```

* All scripts located in ```EIT/js/app.js | app.min.js```
* Main ```sass``` files located in ```EIT/styles/sass/styles.*```
* Each ```html component``` located in ```EIT/components```

## Include parts of HTML code:
 You can import any part of the code using construction in any of html files:

 `<!--#include virtual="/components/GC001-Header.html" -->`

## Carousels
Slick - http://kenwheeler.github.io/slick/ 

## Accordions
Beefup - https://schascha.github.io/BeefUp/

## Animations
Animate CSS - https://animate.style/

## Video
jQuery Modal Video - https://appleple.github.io/modal-video/