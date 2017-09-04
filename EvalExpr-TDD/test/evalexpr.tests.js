var should = require('should');
var eval = require('../source/evalexpr');

describe('eval expr', function () {
    it('should eval exist', function () {
        should(eval).be.ok;
    });

    describe('addition', function () {
        it('should return 3 for 1+2', function () {
            should(eval.evaluateExpression("1+2")).equal(3);
        });

        it('should return 3 for (1+2)', function () {
            should(eval.evaluateExpression("(1+2)")).equal(3);
        });

        it('should return 3.1 for 1.1+2', function () {
            should(eval.evaluateExpression("1.1+2")).equal(3.1);
        });
    });

    describe('substraction', function () {
        it('should return 2 for 3-1', function () {
            should(eval.evaluateExpression("3-1")).equal(2);
        });

        it('should return 2 for (3-1)', function () {
            should(eval.evaluateExpression("(3-1)")).equal(2);
        });

        it('should return 2.1 for 3.1-1', function () {
            should(eval.evaluateExpression("3.1-1")).equal(2.1);
        });
    });

    describe('multiplication', function () {
        it('should return 4 for 2*2', function () {
            should(eval.evaluateExpression("2*2")).equal(4);
        });

        it('should return 4.2 for 2.1*2', function () {
            should(eval.evaluateExpression("2.1*2")).equal(4.2);
        });
    });

    describe('division', function () {
        it('should return 0.5 for 1/2', function () {
            should(eval.evaluateExpression("1/2")).equal(0.5);
        });

        it('should return 0.55 for 1.1/2', function () {
            should(eval.evaluateExpression("1.1/2")).equal(0.55);
        });
    });

    describe('different operations', function () {
        it('should return 25.5 for 1/2 + 5 + 4*5', function () {
            should(eval.evaluateExpression("1/2 + 5 + 4*5")).equal(25.5);
        });

        it('should return 45.5 for 1/2 + (5 + 4) * 5', function () {
            should(eval.evaluateExpression("1/2 + (5 + 4) * 5")).equal(45.5);
        });

        it('should return 45.5 for 1/2    + (5 + 4) * 5', function () {
            should(eval.evaluateExpression("1/2    + (5 + 4) * 5")).equal(45.5);
        });
    });
});