/******/ (function() { // webpackBootstrap
/******/ 	"use strict";
/******/ 	// The require scope
/******/ 	var __webpack_require__ = {};
/******/ 	
/************************************************************************/
/******/ 	/* webpack/runtime/make namespace object */
/******/ 	!function() {
/******/ 		// define __esModule on exports
/******/ 		__webpack_require__.r = function(exports) {
/******/ 			if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 				Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 			}
/******/ 			Object.defineProperty(exports, '__esModule', { value: true });
/******/ 		};
/******/ 	}();
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// ESM COMPAT FLAG
__webpack_require__.r(__webpack_exports__);

;// CONCATENATED MODULE: external "@bytescale/upload-widget"
var upload_widget_namespaceObject = require("@bytescale/upload-widget");;
;// CONCATENATED MODULE: ./src/index.ts
function _typeof(obj) { "@babel/helpers - typeof"; return _typeof = "function" == typeof Symbol && "symbol" == typeof Symbol.iterator ? function (obj) { return typeof obj; } : function (obj) { return obj && "function" == typeof Symbol && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj; }, _typeof(obj); }



(function ($) {
  var funcs = $.fn;

  funcs.bytescaleUploadWidget = function (options) {
    var _a, _b;

    var handleOnComplete = function handleOnComplete(promise) {
      promise.then(function (files) {
        var onComplete = options.onComplete;

        if (onComplete !== undefined) {
          onComplete(files);
        }
      }, function (e) {
        return console.error("[bytescale-upload-widget] Unexpected error.", e);
      });
    };

    if (options.layout === "inline" || options.dropzone === true || _typeof(options.dropzone) === "object") {
      var dropzone = _typeof(options.dropzone) === "object" ? options.dropzone : undefined;
      $(this).css({
        position: "relative",
        width: "100%",
        minWidth: "280px",
        maxWidth: (_a = dropzone === null || dropzone === void 0 ? void 0 : dropzone.width) !== null && _a !== void 0 ? _a : "600px",
        height: (_b = dropzone === null || dropzone === void 0 ? void 0 : dropzone.height) !== null && _b !== void 0 ? _b : "375px"
      });
      $(this).get().forEach(function (element) {
        handleOnComplete(upload_widget_namespaceObject.UploadWidget.open(Object.assign(Object.assign({}, options), {
          layout: "inline",
          container: element
        })));
      });
    } else {
      $(this).on("click", function () {
        handleOnComplete(upload_widget_namespaceObject.UploadWidget.open(options));
      });
    }

    return this;
  };
})(jQuery);
module.exports = __webpack_exports__;
/******/ })()
;