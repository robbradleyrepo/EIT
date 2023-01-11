#LionTrust Flats Starter

## Start project
for start project clone this repository and run ```npm i```

# Update 2023

FE Env has been updated to incorporate EIT/ project which sits parallel to app/ (LT as before)

## Main Gulpfile.js options:

## LionTrust Project:

Unaffected by update, except for new gulp commands:

* ```gulp lt``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```gulp build```: project build


## Edinburgh Investment Trust Project:

* ```gulp eit``` : run default gulp task (scripts, images, styles, browsersync, startwatch)
* ```gulp build```: project build

```gulp build``` command will execute BOTH projects concurrently.

Both projects work successfully in isolation from one another as reflected by isolated
gulp tasks, refactored and extended accordingly.

## Destination dirs

* LT: ```dist/LT/``` 
* EIT: ```dist/EIT/``` 

## Asset Paths (both builds)

* HTML: ```<img src="images/***" />```
* SASS: ```background-image: url("../images/***")```

## EIT Specific

Bootstrap has been stripped out. 

New Breakpoint, Spacing and Grid system now work inconjunction, allowing nested mixins to play nice.

# Grid

GridilyDidily

```EIT\styles\sass\vendor\gridilydidily.scss```

http://philippkuehn.github.io/gridilydidily/

# Spacings

Sass Spacing

```EIT\styles\sass\vendor\sass-spacing.scss```

https://github.com/digitaledgeit/sass-spacing

# Breakpoints

```EIT\styles\sass\core\_breakpoints.sass```

# BEMIT 

Class prefixes

```t-***``` - Template
```u-***``` - Utility (Containers, Site Wrappers, Generic)
```g-***``` - Component (Global)
```c-***``` - Component (Local)
```e-***``` - Element


## Rules
* All scripts located in ```app/js/app.js | app.min.js```
* Main ```sass``` files located in ```app/styles/sass/styles.*```
* Each ```html component``` located in ```app/components```

* All scripts located in ```EIT/js/app.js | app.min.js```
* Main ```sass``` files located in ```EIT/styles/sass/styles.*```
* Each ```html component``` located in ```EIT/components```

##Include parts of HTML code:
 You can import any part of the code using construction in any of html files:

 `<!--#include virtual="/components/GC001-Header.html" -->`