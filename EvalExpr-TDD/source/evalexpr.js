const operators = {
    '+': function(x, y) {return x + y},
    '-': function(x, y) {return x - y},
    '*': function(x, y) {return x * y},
    '/': function(x, y) {return x / y},
    ')': function () {},
    '(': function () {}
};
const priority = {
    '(': 0,
    ')': 0,
    '+': 1,
    '-': 1,
    '*': 2,
    '/': 2
};

var eval = {
    evaluateExpression: function (str) {

        var operands = [];
        var stack = [];
        var opened = false;

        if (str.search(/^[\d\s.+\-()\/*]+$/) === -1)
             return undefined;
        if (str.search(/[+\-\/*][+\-\/*]+/) !== -1)
            return undefined;

        for(var i = 0; i < str.length; i++)
        {
            while (str[i] === ' ')
                i++;

            if (str[i] in operators)
            {
                if (str[i] === ')')
                {
                    opened = false;
                    var op = operands.pop();
                    while (op !== '(')
                    {
                        stack.push(op);
                        op = operands.pop();
                    }
                }
                else {
                    if (str[i] !== '(') {
                        if (str[i] === '-' &&
                            (str.substring(i < 1? 0 : i - 1).search(/^\d-\d/) === -1
                            && str.substring(i < 1? 0 : i - 1).search(/^ - /) === -1))
                        {
                            var nb = parseFloat(str.substring(i));
                            stack.push(nb);
                            i += nb.toString().length;
                        }
                        else {
                            var lastOperand = operands[operands.length - 1];

                            while (priority[str[i]] <= priority[lastOperand]) {
                                stack.push(operands.pop());
                                lastOperand = operands[operands.length - 1];
                            }
                        }
                    }
                    else
                        opened = true;
                    if (str[i] !== ' ')
                        operands.push(str[i]);
                }
            }
            else {
                nb = parseFloat(str.substring(i));
                stack.push(nb);
                i += nb.toString().length - 1;
            }
        }
        if (opened)
            return undefined;

        while (operands.length > 0)
            stack.push(operands.pop());

        var outputStack = [];

        stack.forEach(function (token) {
            if (token in operators)
            {
                var n2 = outputStack.pop(),
                    n1 = outputStack.pop();
                outputStack.push(operators[token](n1, n2));
            }
            else {
                outputStack.push(token);
            }
        });

        if (outputStack.length > 1)
            return undefined;
        return +outputStack.pop().toFixed(2);
    }
};

module.exports = eval;