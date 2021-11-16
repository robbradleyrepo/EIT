let preprocessor = "sass", // Preprocessor (sass, less, styl); 'sass' also work with the Scss
  fileswatch = "html,htm,txt,json,md,woff2"; // List of files extensions for watching & hard reload

const { src, dest, parallel, series, watch } = require("gulp");
const browserSync = require("browser-sync").create();
const bssi = require("browsersync-ssi");
const ssi = require("ssi");
const webpack = require("webpack-stream");
const sass = require("gulp-sass");
const sassglob = require("gulp-sass-glob");
const less = require("gulp-less");
const lessglob = require("gulp-less-glob");
const styl = require("gulp-stylus");
const stylglob = require("gulp-noop");
const cleancss = require("gulp-clean-css");
const autoprefixer = require("gulp-autoprefixer");
const rename = require("gulp-rename");
const imagemin = require("gulp-imagemin");
const newer = require("gulp-newer");
const rsync = require("gulp-rsync");
const del = require("del");
const uglify = require('gulp-uglify');

const webPackConfig = {
  mode: "development",
  performance: { hints: false },
  module: {
    rules: [
      {
        test: /\.(js)$/,
        exclude: /(node_modules)/,
        loader: "babel-loader",
        query: {
          presets: [
            [
              "@babel/preset-env",
              {
                useBuiltIns: "usage", // or "entry"
                corejs: 3,
              },
            ],
          ],
          plugins: ["babel-plugin-root-import"],
        },
      },
    ],
  },
};

function browsersync() {
  browserSync.init({
    server: {
      baseDir: "app/",
      middleware: bssi({ baseDir: "app/", ext: ".html" }),
    },
    ghostMode: { clicks: false },
    notify: false,
    online: true,
    // tunnel: 'yousutename'
  });
}

function scriptsMain() {
  return src(
    [
      "app/js/app.js",
      "node_modules/@fancyapps/fancybox/dist/jquery.fancybox.min.js", // import fancybox
      "node_modules/bootstrap/js/dist/modal.js", // import bootstrap modal
      "node_modules/bootstrap/js/dist/collapse.js", // import bootstrap collapse
      "node_modules/bootstrap/js/dist/tooltip.js", // import tooltip
    ],
    { sourcemaps: true }
  )
    .pipe(webpack(webPackConfig))
    .on("error", function handleError() {
      this.emit("end");
    })
    .pipe(rename("app.min.js"))
    .pipe(dest("app/js", { sourcemaps: true }))
    .pipe(browserSync.stream());
}

function scriptsSearch() {
  return src(["app/js/search-page.js"], {
    sourcemaps: true,
  })
    .pipe(webpack(webPackConfig))
    .on("error", function handleError() {
      this.emit("end");
    })
    .pipe(rename("search-page.min.js"))
    .pipe(dest("app/js", { sourcemaps: true }))
    .pipe(browserSync.stream());
}

function scriptsListing() {
  return src(["app/js/listing-page.js"], {
    sourcemaps: true,
  })
    .pipe(webpack(webPackConfig))
    .on("error", function handleError() {
      this.emit("end");
    })
    .pipe(rename("listing-page.min.js"))
    .pipe(dest("app/js", { sourcemaps: true }))
    .pipe(browserSync.stream());
}

function scriptsCharts() {
  return src(["app/js/charts-page.js"], {
    sourcemaps: true,
  })
    .pipe(webpack(webPackConfig))
    .on("error", function handleError() {
      this.emit("end");
    })
    .pipe(rename("charts-page.min.js"))
    .pipe(dest("app/js", { sourcemaps: true }))
    .pipe(browserSync.stream());
}


function styles() {
  return src(
    [`app/styles/${preprocessor}/*.*`, `!app/styles/${preprocessor}/_*.*`],
    { sourcemaps: true }
  )
    .pipe(eval(`${preprocessor}glob`)())
    .pipe(eval(preprocessor)())
    .pipe(
      autoprefixer({ overrideBrowserslist: ["last 10 versions"], grid: true })
    )
    .pipe(
      cleancss({
        level: { 1: { specialComments: 0 } } /* format: 'beautify' */,
      })
    )
    .pipe(rename({ suffix: ".min" }))
    .pipe(dest("app/css", { sourcemaps: true }))
    .pipe(browserSync.stream());
}

function images() {
  return src(["app/images/src/**/*"])
    .pipe(newer("app/images/dist"))
    .pipe(imagemin())
    .pipe(dest("app/images/dist"))
    .pipe(browserSync.stream());
}

function minifyJs() {
  return src(['app/js/*.min.js'])
  .pipe(uglify())
  .pipe(dest('app/js'))
}
 
function buildcopy() {
  return src(
    [
      "{app/js,app/css}/*.min.*",
      "app/images/**/*.*",
      "!app/images/src/**/*",
      "app/fonts/**/*",
	  "app/js/chartjs-plugin-datalabels@0.7.0.js"
    ],
    { base: "app/" }
  ).pipe(dest("dist"));
}

async function buildhtml() {
  let includes = new ssi("app/", "dist/", "/**/*.html");
  includes.compile();
  del("dist/components", { force: true });
}

function cleandist() {
  return del("dist/**/*", { force: true });
}

function startwatch() {
  watch(`app/styles/${preprocessor}/**/*`, { usePolling: true }, styles);
  watch(
    ["app/js/**/*.js", "!app/js/**/*.min.js"],
    { usePolling: true },
    parallel(scriptsMain, scriptsSearch, scriptsListing, scriptsCharts)
  );
  watch(
    "app/images/src/**/*.{jpg,jpeg,png,webp,svg,gif}",
    { usePolling: true },
    images
  );
  watch(`app/**/*.{${fileswatch}}`, { usePolling: true }).on(
    "change",
    browserSync.reload
  );
}

exports.scripts = series(scriptsMain, scriptsSearch, scriptsListing, scriptsCharts);
exports.styles = styles;
exports.images = images;
exports.assets = series(scriptsMain, scriptsSearch, scriptsListing, scriptsCharts, styles, images);
exports.build = series(
  cleandist,
  scriptsMain,
  scriptsSearch,
  scriptsListing,
  scriptsCharts,
  minifyJs,
  styles,
  images,
  buildcopy,
  buildhtml
);
exports.default = series(
  scriptsMain,
  scriptsSearch,
  scriptsListing,
  scriptsCharts,
  styles,
  images,
  parallel(browsersync, startwatch)
);
