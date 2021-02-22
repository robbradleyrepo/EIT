/* 
 * ---------------------------------------- *
 * Name: Codehouse Carousel JavaScripts     *
 * Version: 3.0.2							*
 * Author: Matt O'Neill                     *
 * Status: Release							*
 * Requisites: >=jquery ui 1.8.20           *
 *             chg.Cookie(opt)              *
 *             json2(opt IE7 Cookie)        *
 * ---------------------------------------- *
 */

void function ($) {
    'use strict';
    $.CodehouseCarousel = function (el, settings, inc) {
        var app = this,
            defaults = {
                modes: { slide: true, infinite: true, responsive: true, nudge: false }
                , controls: { step: true, pager: true }
                , rotate: { auto: false, direction: 'right', interval: 500, duration: 300, type: 'linear' }
                , dimensions: { heightControl: true, fixedHeight: true, maxHeight: 400, baseWidth: 600, slidesInFrame: 1 }
                , options: { preload: false, stickySlides: true, visibleClassAfter: true, maskedOverflow: true, setSlide: 0, touchControl: true }
                , onReady: function () { }
                , onRotateStart: function () { }
                , onRotateEnd: function () { }
            },
            s = merge(defaults, settings);

        /*
         * access slider data
         *
         * @property actionData
         * @static 
         * @param wth {Int} forced width 
         */
        app.actionData = {};

        /*
         * Application Namespace
         *
         * @class cc
         * @static
         */
        var cc = {
            m: {
                $carousel: $(el)
                , isAnimating: false
                , heroMode: false
                , autoDisabled: false
                , modes: []
                , firstPosition: 0
                , lastPosition: 0
                , cycleTimer: 0
                , activeIndex: 0
                , currentPosition: 0
                , overflow: 0
                , slideCount: 0
                , slideHeight: undefined
                , slideWidth: undefined
                , dynamicSlideWidth: null
                , divisor: 1
                , imgSrcArr: []
                , trans: {}
                , resEv: 'resize'
                , algFct: {
                    'linear': ['cubic-bezier(0.250, 0.250, 0.750, 0.750)', 'linear']
                    , 'quad': ['cubic-bezier(0.455, 0.030, 0.515, 0.955)', 'easeInOutQuad']
                    , 'back': ['cubic-bezier(0.680, -0.550, 0.265, 1.550)', 'easeInOutBack']
                    , 'expo': ['cubic-bezier(1.000, 0.000, 0.000, 1.000)', 'easeInOutExpo']
                }
                , ui: {
                    masks: '<span class="mask left" data-direction="left"></span><span class="mask right" data-direction="right"></span>'
                }
                , stateData: { activeSlide: 0 }
                , browser: {
                    oldIe: false
                }
                , _constr: function () {
                    var navAgent = navigator.userAgent.toLowerCase(); // browser 
                    cc.m.browser.oldIe = navAgent.indexOf('msie 7.0') > -1 || navAgent.indexOf('msie 8.0') > -1;
                    cc.m.ccId = 'cc-id-' + cc.m.$carousel.data('guid');
                    app.actionData = { $banner: $(el), slideDirection: s.rotate.direction, active: undefined };
                }
                , _configImg: function () {
                    var prldImgArr = [];
                    for (var x = 0; x < cc.m.$slide.length; x++) {
                        cc.m.imgSrcArr[x] = cc.m.$slide.eq(x).find('img').attr('src');
                    }
                    if (cc.m.imgSrcArr.length >= 1) {
                        for (var a in cc.m.imgSrcArr) {
                            prldImgArr[a] = new Image();
                            prldImgArr[a].onload = cc.c.async._updChk();
                            prldImgArr[a].onerror = cc.c.async._updChk();
                            prldImgArr[a].src = cc.m.imgSrcArr[a];
                        }
                    }
                },
                _props: function (partial) {
                    cc.m.heroMode = cc.m.$slide.length <= 1;
                    cc.m.divisor = !cc.m.heroMode ? s.modes.slide && s.modes.infinite ? 3 : 1 : 0;

                    if (!partial) {
                        cc.m.slideCount = cc.m.divisor > 0 ? (cc.m.$slide.length * cc.m.divisor) / cc.m.divisor : 0;
                    } else {
                        cc.m.slideCount = cc.m.$slide.length / cc.m.divisor;
                    }

                    cc.m.slideHeight = cc.m.$carousel.parent().height();
                    cc.m.slideWidth = cc.m.dynamicSlideWidth = (cc.m.$carousel.parent().width() / s.dimensions.slidesInFrame);

                    cc.m.trans = cc.m.fact._transType();
                    cc.m.modes = [];

                    //cc.m.trans.prefix = undefined; // @ temp switch between margin / css3 
                },
                _staticCache: function () {
                    this.defaults.cookie = 'Cookie' in window ? Object.build(new Cookie()) : undefined;
                    cc.m.$viewport = cc.m.$carousel.find('.viewport');
                    cc.m.$slides = cc.m.$carousel.find('.slide-container');
                    cc.m.$slide = cc.m.$slides.find('div.slide');
                    cc.m.$pagerContainer = cc.m.$carousel.find('.pagination-controls');
                    cc.m.$stepControls = cc.m.$carousel.find('.step');
                    cc.m.$masks = cc.m.$carousel.find('.mask');
                },
                // update slide count
                _dynamicCache: function () {
                    delete cc.m.$slide;
                    cc.m.$slide = cc.m.$slides.children('div');
                },
                _stitch: function () { // build dynamic markup
                    cc.m.$carousel.prepend(cc.m.ui.masks); // cc.m.overflow masks
                },
                defaults: {
                    _get: function () {
                        if (s.options.stickySlides && this.cookie !== undefined) {
                            try {
                                this.defaultsData = this.cookie.check(cc.m.ccId);
                            } catch (e) { }
                            if (this.defaultsData !== undefined) {
                                cc.m.stateData = JSON.parse(this.defaultsData);
                            }
                        }
                    },
                    _set: function () {
                        if (s.options.stickySlides && this.cookie !== undefined) {
                            try {
                                this.cookie.write(cc.m.ccId, JSON.stringify(cc.m.stateData));
                            } catch (e) { }
                        }
                    },
                    _apply: function () {
                        // user set slide
                        if (s.options.setSlide > 0 && s.options.setSlide <= cc.m.slideCount) {
                            cc.c._actionController(s.options.setSlide - 1, false);
                        } else { // cookie or application default
                            cc.m.stateData.activeSlide > 0 ? cc.c._actionController(cc.m.stateData.activeSlide, false) : app.actionData.active = 1;
                        }
                    }
                },
                carousel: {
                    _slideVals: function (vector) {
                        var indexDif = 1,
                            posData,
                            typedVector = 0;
                        if (typeof vector == 'number') {
                            if (vector < cc.m.activeIndex) {
                                indexDif = -(vector - cc.m.activeIndex);
                                typedVector = 'left';
                            } else if (vector > cc.m.activeIndex) {
                                indexDif = -(cc.m.activeIndex - vector);
                                typedVector = 'right';
                            } else if (vector === cc.m.activeIndex) {
                                typedVector = 'reset';
                            }
                        } else {
                            typedVector = vector;
                        }
                        app.actionData.slideDirection = typedVector;
                        posData = cc.m.fact.calc[typedVector](indexDif);
                        cc.m.activeIndex = typeof vector == 'number' ? vector :
                            posData[1] ? (cc.m.activeIndex == Math.ceil(cc.m.slideCount) - 1) ? 0 : cc.m.activeIndex + 1 :
                            (cc.m.activeIndex == 0) ? Math.ceil(cc.m.slideCount) - 1 : cc.m.activeIndex - 1;
                        app.actionData.active = cc.m.activeIndex + 1;
                        return { newPosition: posData[0], dir: posData[1] };
                    },
                    _nudgeVals: function (vector) {
                        return cc.m.currentPosition + { 'left': 20, 'right': -20, 'null': 0 }[vector];
                    },
                    _sizingVals: function (wth) {
                        cc.m.dynamicSlideWidth = wth;
                        cc.m.overflow = s.modes.infinite ? cc.m.dynamicSlideWidth * (Math.ceil(cc.m.slideCount)) * -1 : 0;
                        cc.m.firstPosition = cc.m.overflow;
                        cc.m.lastPosition = cc.m.overflow - (cc.m.dynamicSlideWidth * (cc.m.slideCount) - cc.m.dynamicSlideWidth);
                        cc.m.currentPosition = cc.m.firstPosition - cc.m.dynamicSlideWidth * cc.m.activeIndex;
                    }
                },
                fact: {
                    _transType: function () {

                        var div = document.createElement('div'),
                            props = ['WebkitPerspective', 'MozPerspective', 'OPerspective', 'msPerspective'];
                        for (var i in props) {
                            if (div.style[props[i]] !== undefined) {
                                return {
                                    prefix: props[i].replace('Perspective', '').toLowerCase(),
                                    anim: '-' + props[i].replace('Perspective', '').toLowerCase() + '-transform'
                                };
                            }
                            if (+i === (props.length - 1)) {
                                return {
                                    prefix: undefined,
                                    anim: 'margin-left'
                                };
                            }
                        }
                    },
                    _heightRatio: function (dW) {
                        var heightRatio = s.dimensions.maxHeight / s.dimensions.baseWidth * dW;
                        return s.dimensions.fixedHeight ? parseInt(s.dimensions.maxHeight) :
                                                          heightRatio > s.dimensions.maxHeight ? s.dimensions.maxHeight : heightRatio; // fixed height ? 
                    },
                    _getSlideWidth: function () {
                        return (cc.m.$viewport.width() / s.dimensions.slidesInFrame);
                    },
                    calc: {
                        left: function (indexDif) {
                            return [s.modes.slide ? s.modes.infinite ? cc.m.currentPosition + (cc.m.dynamicSlideWidth * indexDif) : cc.m.activeIndex == 0 ? cc.m.lastPosition : cc.m.currentPosition + (cc.m.dynamicSlideWidth * indexDif) : 0, false];
                        },
                        right: function (indexDif) {
                            return [s.modes.slide ? s.modes.infinite ? cc.m.currentPosition - (cc.m.dynamicSlideWidth * indexDif) : cc.m.activeIndex == cc.m.slideCount - 1 ? cc.m.firstPosition : cc.m.currentPosition - (cc.m.dynamicSlideWidth * indexDif) : 0, true];
                        },
                        reset: function () {
                            return [s.modes.slide ? cc.m.currentPosition : 0, true];
                        }
                    }
                },
                _errors: function () { // fault application
                    cc.m.isError = true;
                }
            },
            c: {
                async: {
                    _loadIncrementor: 0,
                    _preload: function () {
                        s.options.preload && !cc.m.browser.oldIe ? cc.m._configImg() : cc.c._ready();
                    },
                    _updChk: function () {
                        this._loadIncrementor++;
                        if (this._loadIncrementor === cc.m.imgSrcArr.length) {
                            cc.c._ready();
                        }
                    }
                },
                _configure: function (partial) {
                    cc.m._props(partial); // property setter
                    cc.m.$slides.attr('data-totalslides', cc.m.$slide.length);
                    if (s.modes.slide && s.modes.infinite && !cc.m.heroMode) {
                        cc.m.modes.push('infinite');
                        if (!partial) {
                            cc.m.$slides.prepend(cc.m.$slide.clone());
                            cc.m.$slides.prepend(cc.m.$slide.clone());
                        }
                    }

                    cc.m.modes.push(s.modes.slide ? 'slide' : 'fade');
                    cc.m._dynamicCache(); // update cache
                    cc.v._applyModes();
                    cc.c._fit();

                    void ((s.controls.pager && !cc.m.heroMode && !partial) && cc.v.carousel._buildPager());
                    cc.v.carousel._toggleCntr();
                    cc.v.carousel._togglePager();
                    cc.v.carousel._toggleMasks();
                    void (s.controls.pager && cc.v.carousel._setActivePager());
                    cc.c.autoScroll._intTmr();
                },
                _ready: function () {
                    if (!cc.m.isError) {
                        cc.c._dynEvnts();
                        cc.m.defaults._apply();
                        cc.v._setActive();
                        cc.v._present();
                        cc.v.carousel._stVis();
                        cc.c.external._ready();
                    }
                },
                _staticEvents: function () {
                    var tmr;
                    cc.m.$carousel.on({
                        mouseenter: function () {
                            cc.m.autoDisabled = true;
                        },
                        mouseleave: function () {
                            cc.m.autoDisabled = false;
                        }
                    });

                    $(window).on(cc.m.resEv, function (e) {
                        cc.m.$slides.stop();
                        cc.m.autoDisabled = true;
                        clearTimeout(tmr);
                        cc.c._responsive();
                        tmr = setTimeout(function () {
                            end();
                        }, 100);
                    });
                    function end() {
                        cc.m.isAnimating = false;
                        cc.c._responsive();
                        cc.m.autoDisabled = false;
                    }
                },
                _dynEvnts: function () {

                    if (s.controls.step) {
                        cc.m.$stepControls.on({
                            click: function () {
                                cc.c._actionController($(this).data('direction').toLowerCase(), true);
                            }
                        });
                    } else {
                        cc.m.$stepControls.off('click');
                    }

                    cc.m.$stepControls.off('mouseenter mouseleave');
                    void (s.modes.nudge && cc.m.$stepControls.on({
                        mouseenter: function () {
                            if (!cc.m.isAnimating && s.modes.slide) {
                                cc.c._nudge($(this).data('direction').toLowerCase());
                            }
                        },
                        mouseleave: function () {
                            if (!cc.m.isAnimating && s.modes.slide) {
                                cc.c._nudge(null);
                            }
                        }
                    }));

                    void ((s.controls.pager && !cc.m.heroMode) && cc.m.$pagerContainer.children().on({
                        click: function () {
                            var idx = $(this).index();
                            if (idx != cc.m.activeIndex) {
                                cc.c._actionController(idx, true);
                            }
                        }
                    }));
                    if (s.options.touchControl === true) {
                        void (('touch' in window && cc.m.trans.prefix && !cc.m.heroMode) && cc.m.$slides.touch({
                            threshold: 100,
                            swipeDirection: 'horizontal',
                            start: function (d) {
                                cc.m.$slides.css('-' + cc.m.trans.prefix + '-transition-duration', '0s');
                            },
                            right: function () {
                                cc.c._actionController('left', true);
                            },
                            left: function () {
                                cc.c._actionController('right', true);
                            },
                            moving: function (d) {
                                if (!cc.m.isAnimating && s.modes.slide) {
                                    cc.v.action._move(d.x);
                                }
                            },
                            notReached: function () {
                                if (s.modes.slide) {
                                    cc.c._actionController(cc.m.activeIndex, true);
                                }
                            }
                        }));
                    }
                },
                /*
                 * handle carousel position actions
                 *
                 * @method _actionController
                 * @private
                 * @param vector { Loose Equivalent } number or string representing index or direction
                 * @param anim { Boolean } snap or slide
                 * @returns void
                 */
                _actionController: function (vector, anim) {
                    if (!cc.m.isAnimating) {
                        cc.c.autoScroll._clearTimer();
                        cc.m.isAnimating = true;
                        var params = cc.m.carousel._slideVals(vector);
                        cc.c.external._start();
                        cc.v.carousel._setActivePager();
                        void (!s.options.visibleClassAfter && cc.v.carousel._stVis());
                        if (s.modes.slide) {
                            if (anim) {
                                cc.v.action._slide(params.newPosition, function () {
                                    if (s.modes.infinite && (cc.m.activeIndex === 0 && params.dir) || (cc.m.activeIndex === Math.ceil(cc.m.slideCount) - 1) && !params.dir) { // reset
                                        cc.m.currentPosition = params.dir ? cc.m.firstPosition : cc.m.lastPosition;
                                        cc.v.action._snap(cc.m.currentPosition);
                                    } else {
                                        cc.m.currentPosition = params.newPosition;
                                    }
                                    complete();
                                });
                            } else {
                                cc.v.action._snap(params.newPosition);
                                if (s.modes.infinite && (cc.m.activeIndex === 0 && params.dir) || (cc.m.activeIndex === Math.ceil(cc.m.slideCount) - 1) && !params.dir) { // reset
                                    cc.m.currentPosition = params.dir ? cc.m.firstPosition : cc.m.lastPosition;
                                } else {
                                    cc.m.currentPosition = params.newPosition;
                                }
                                complete();
                            }
                        } else {
                            if (anim) {
                                cc.v.action._fade(complete);
                            } else {
                                cc.v.action._snap(params.newPosition); // snap
                                cc.v.action._show(complete);
                            }
                        }
                    }
                    function complete() {
                        cc.m.isAnimating = false;
                        cc.c.autoScroll._intTmr();
                        cc.c.external._end();
                        void (s.options.visibleClassAfter && cc.v.carousel._stVis());
                    }
                    cc.m.stateData.activeSlide = cc.m.activeIndex;
                    cc.m.defaults._set();
                },
                _nudge: function (vector) {
                    cc.v.action._nudge(cc.m.carousel._nudgeVals(vector));
                },
                /*
                 * carousel component dimension setter
                 *
                 * @method _fit
                 * @private
                 * @param wth {Int} forced width 
                 */
                _fit: function (wth) {
                    wth = wth === undefined ? cc.m.fact._getSlideWidth() : wth;

                    if (s.options.heightControl) {
                        cc.v.dimensions._setHeight(wth); // set height
                    }

                    cc.v.dimensions._setWidth(wth); // set widths                   
                    if (s.modes.slide) {
                        cc.m.carousel._sizingVals(wth);
                        cc.v.action._snap(cc.m.overflow - (cc.m.dynamicSlideWidth * cc.m.activeIndex));
                        if (!cc.m.heroMode) {
                            cc.v.dimensions._masks(); // set new mask dimensions
                        }
                    }

                    if (!cc.m.heroMode) {
                        cc.v.dimensions._masks(); // set new mask dimensions
                    }
                },
                _responsive: function () {
                    if (s.modes.responsive) {
                        cc.c._fit(cc.m.fact._getSlideWidth());
                    }
                },
                autoScroll: {
                    _intTmr: function () {
                        if (s.rotate.auto && !cc.m.heroMode) {
                            cc.c.autoScroll._clearTimer();
                            cc.m.cycleTimer = setInterval(function () {
                                if (!cc.m.autoDisabled) {
                                    cc.c._actionController(s.rotate.direction.toLowerCase(), true);
                                }
                            }, s.rotate.interval);
                        }
                    },
                    _clearTimer: function () {
                        clearInterval(cc.m.cycleTimer);
                    }
                },
                external: {
                    _start: function () {
                        s.onRotateStart(app.actionData);
                    },
                    _end: function () {
                        s.onRotateEnd(app.actionData);
                    },
                    _ready: function () {
                        s.onReady(app.actionData);
                    }
                }
            },
            v: {
                carousel: {
                    _toggleCntr: function () {
                        (!cc.m.heroMode && s.controls.step) ? cc.m.$stepControls.fadeIn(800) : cc.m.$stepControls.hide();
                    },
                    _setActivePager: function () {
                        cc.m.$pagerContainer.children().removeClass('active').eq(cc.m.activeIndex).addClass('active');
                    },
                    _stVis: function () {
                        cc.m.$slide.removeClass('visible').eq(s.modes.slide ? cc.m.activeIndex + Math.ceil(cc.m.slideCount) : cc.m.activeIndex).addClass('visible');
                    },
                    _buildPager: function () {
                        for (var i = 0; i < cc.m.slideCount; i++) {
                            cc.m.$pagerContainer.append('<span />');
                        }
                        cc.m.$pagerContainer.css({
                            marginLeft: -(cc.m.$pagerContainer.outerWidth() / 2)
                        });
                    },
                    _togglePager: function () {
                        (!cc.m.heroMode && s.controls.pager) ? cc.m.$pagerContainer.show() : cc.m.$pagerContainer.hide();
                    },
                    _replaceContent: function (content) {
                        cc.m.$slides.html(content);
                        cc.m.$pagerContainer.empty();
                    },
                    _toggleMasks: function () {
                        s.options.maskedOverflow ? cc.m.$masks.fadeIn(800) : cc.m.$masks.hide();
                    }
                },
                action: {
                    _fade: function (update) {
                        cc.m.$slides.children('.active').css('z-index', 1);
                        cc.m.$slides.children().eq(cc.m.activeIndex).css('z-index', 2).fadeIn(s.rotate.duration, function () {
                            cc.m.$slides.children('.active').css('z-index', 0).hide().removeClass('active');
                            $(this).addClass('active');
                            update();
                        });
                    },
                    _show: function (update) {
                        cc.m.$slides.children().eq(0).hide();
                        cc.m.$slides.children().eq(cc.m.activeIndex).css('z-index', 2).show().addClass('active');
                        update();
                    },
                    _slide: function (pos, update) {
                        switch (cc.m.trans.prefix) {
                            case undefined:
                                cc.m.$slides.stop().animate({
                                    marginLeft: pos
                                }, s.rotate.duration, cc.m.algFct[s.rotate.type][1], function () {
                                    update();
                                });
                                break;
                            default:
                                cc.m.$slides.css('-' + cc.m.trans.prefix + '-transition-duration', (s.rotate.duration / 1000) + 's')
                                    .css(cc.m.trans.anim, 'translate3d(' + pos + 'px, 0, 0)')
                                    .css('-' + cc.m.trans.prefix + '-transition-timing-function', cc.m.algFct[s.rotate.type][0])
                                    .on('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend', function () {
                                        $(this).unbind('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend');
                                        update();
                                    });
                                break;
                        }
                    },
                    _snap: function (end) {
                        switch (cc.m.trans.prefix) {
                            case undefined:
                                cc.m.$slides.css('margin-left', end);
                                break;
                            default:
                                cc.m.$slides.css('-' + cc.m.trans.prefix + '-transition-duration', '0s');
                                cc.m.$slides.css(cc.m.trans.anim, 'translate3d(' + end + 'px, 0, 0)');
                                break;
                        }
                    },
                    _move: function (xPos) {
                        cc.m.$slides.css(cc.m.trans.anim, 'translate3d(' + (cc.m.currentPosition + xPos) + 'px, 0, 0)');
                    },
                    _nudge: function (pos) {
                        switch (cc.m.trans.prefix) {
                            case undefined:
                                cc.m.$slides.stop().animate({
                                    marginLeft: pos
                                }, 150, 'linear');
                                break;
                            default:
                                cc.m.$slides.css('-' + cc.m.trans.prefix + '-transition-duration', 0.15 + 's')
                                    .css(cc.m.trans.anim, 'translate3d(' + pos + 'px, 0, 0)')
                                    .css('-' + cc.m.trans.prefix + '-transition-timing-function', 'cubic-bezier(0.250, 0.250, 0.750, 0.750)');
                                break;
                        }
                    }
                },
                _loading: function () {
                    cc.m.$carousel.children('span.loader').fadeIn(300);
                },
                _present: function () {
                    cc.m.$carousel.children('span.loader').fadeOut(600);
                },
                _applyModes: function () {
                    cc.m.$carousel.attr('data-modes', '');
                    cc.m.$carousel.attr('data-modes', cc.m.modes.join(' '));
                },
                _setActive: function () {
                    cc.m.$slide.eq(cc.m.activeIndex).addClass('active');
                },
                dimensions: {
                    _setHeight: function (x) {
                        cc.m.$carousel.add(cc.m.$slides).add(cc.m.$slide).css('height', cc.m.fact._heightRatio(x));
                    },
                    _setWidth: function (x) {
                        cc.m.$slide.css('width', x);
                    },
                    _masks: function () {
                        cc.m.$carousel.children('.mask.left').css({ width: cc.m.overflow * -1, marginLeft: cc.m.overflow });
                        cc.m.$carousel.children('.mask.right').css({ width: cc.m.overflow * -1, marginLeft: cc.m.dynamicSlideWidth });
                    }
                }
            }
        };

        /*
         * invoke the carousel component dimension setter eg. $el.data('codehouseCarousel').fit();
         *
         * @method fit
         * @public
         * @param wth {Int} forced width 
         * @returns void
         */
        this.fit = function (wth) {
            cc.c._fit(wth);
        };

        /*
         * run initialisation sequence
         *
         * @method init
         * @privileged
         */
        this.init = function () {
            cc.m._constr();                             // constructor
            cc.m._stitch();                             // build dynamic ui
            cc.m._staticCache();                        // cache frontend 
            cc.m.defaults._get();                       // get cookie defaults
            cc.c._configure(false);                     // configure model 
            cc.c._staticEvents();                       // bind static events
            cc.c.async._preload();                      // asyncs
            cc.m.$carousel.attr('data-cc-id', inc);     // identifier
        }();

        /*
         * update the contents of the carousel, assuming delivered in correct structure
         *
         * @method update
         * @public
         * @param content { String } content as html string
         * @param reset { Boolean } reset to first slide
         * @param options { Object } optional options to be applied to configuration
         * @returns void
         */
        this.update = function (content, reset, options) {

            var slideIndex = 0;

            if (options) {
                s = merge(s, options);
                if (options.options.setSlide > 0) {
                    slideIndex = options.options.setSlide;
                }
            }
            var partial = true;
            if (content || '') { // replace content and update
                cc.v._loading();
                cc.v.carousel._replaceContent(content);
                cc.m._dynamicCache(); // update cache
                partial = false;
            }

            cc.c._configure(partial);
            cc.c._dynEvnts();

            if (reset || !s.modes.slide) { // reset or fade, reset to 0 pos
                cc.c._actionController(slideIndex, false);
            }
            cc.v._present();
        };

        /*
         * update the contents of the carousel, assuming delivered in correct structure
         *
         * @method rotate
         * @public
         * 
         * @param dir   {String} || {Number}    direction: as string left or right, as number by index
         * @returns     {Void}
         */
        this.rotate = function (dir) {
            cc.c._actionController(dir, true); // direction, animate
        };
    };
    var appInc = 0;
    $.fn.codehouseCarousel = function (settings) {
        return this.each(function () {
            if (undefined == $(this).data('codehouseCarousel-' + appInc)) {
                var app = new $.CodehouseCarousel(this, settings, appInc++);
                $(this).data('codehouseCarousel', app);
            }
        });
    };

    // merge {}
    function merge(a, b) {
        for (var p in a) {
            if (p in b) {
                if (typeof a[p] == 'object') {
                    for (var n in a[p]) {
                        if (b[p][n] != undefined) {
                            if (b[p][n] != a[p][n]) {
                                b[p][n] = b[p][n];
                            } else {
                                b[p][n] = a[p][n];
                            }
                        } else {
                            b[p][n] = a[p][n];
                        }
                    }
                }
                continue;
            }
            b[p] = a[p];
        }
        return b;
    }

    // object helper
    Object.build = function (o) {
        if (o !== undefined) {
            var initArgs = Array.prototype.slice.call(arguments, 1);
            F.prototype = o;
            return new F();
        }

        function F() {
            if ((typeof o.init === 'function') && initArgs.length) {
                o.init.apply(this, initArgs);
            }
        }
    };

    jQuery.easing['jswing'] = jQuery.easing['swing'];
    jQuery.extend(jQuery.easing,
    {
        def: 'easeInOutQuad',
        swing: function (x, t, b, c, d) {
            return jQuery.easing[jQuery.easing.def](x, t, b, c, d);
        },
        easeInOutQuad: function (x, t, b, c, d) {
            if ((t /= d / 2) < 1) return c / 2 * t * t + b;
            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        },
        easeInOutBack: function (x, t, b, c, d, s) {
            if (s == undefined) s = 1.70158;
            if ((t /= d / 2) < 1) return c / 2 * (t * t * (((s *= (1.525)) + 1) * t - s)) + b;
            return c / 2 * ((t -= 2) * t * (((s *= (1.525)) + 1) * t + s) + 2) + b;
        },
        easeInOutExpo: function (x, t, b, c, d) {
            if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * Math.pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-Math.pow(2, -10 * --t) + 2) + b;
        }
    });

}(jQuery);