var should = require('should');
var eval = require('../source/evalexpr');

describe('eval expr', function () {
    it('should eval exist', function () {
        should(eval).be.ok;
    });

    it('should return 3 for 1+2', function () {
        should(eval.evaluateExpression("1+2")).equal(3);
    });

    it('should return 3 for (1+2)', function () {
        should(eval.evaluateExpression("(1+2)")).equal(3);
    });

    it('should return 3.1 for 1.1+2', function () {
        should(eval.evaluateExpression("1.1+2")).equal(3.1);
    });

    it('should return 2 for 3-1', function () {
        should(eval.evaluateExpression("3-1")).equal(2);
    });

    it('should return 2 for (3-1)', function () {
        should(eval.evaluateExpression("(3-1)")).equal(2);
    });
});