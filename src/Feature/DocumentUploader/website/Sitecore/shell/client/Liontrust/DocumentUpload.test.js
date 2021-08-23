require(["jasmineEnv"], function (jasmineEnv) {
  var setupTests = function () {
    "use strict";

    describe("Given a DocumentUpload model", function () {
      var component = new Sitecore.Definitions.Models.DocumentUpload();

      describe("when I create a DocumentUpload model", function () {
        it("it should have a 'isVisible' property that determines if the DocumentUpload component is visible or not", function () {
          expect(component.get("isVisible")).toBeDefined();
        });

        it("it should set 'isVisible' to true by default", function () {
          expect(DocumentUpload.get("isVisible")).toBe(true);
        });

        it("it should have a 'toggle' function that either shows or hides the DocumentUpload component depending on the 'isVisible' property", function () {
          expect(component.toggle).toBeDefined();
        });
      });
    });
  };

  runTests(jasmineEnv, setupTests);
});