var eval = {
    evaluateExpression: function (str) {
        var operators = {
                '+': function(x, y) {return x + y},
            '-': function(x, y) {return x - y},
            '*': function(x, y) {return x * y},
            '/': function(x, y) {return x / y},
            ')': function () {},
            '(': function () {}
        };
        var priority = {
            '(': 0,
            ')': 0,
            '+': 1,
            '-': 1,
            '*': 2,
            '/': 2
        };

        var operands = [];
        var stack = [];
        var opened = false;

        if (str.search(/^[\d\s.+\-()\/*]+$/) === -1)
             return undefined;
        if (str.search(/[+\-\/*][+\-\/*]+/) !== -1)
            return undefined;
        arr = str;
        //var arr = str.replace(/[\s]/g, "");
        for(var i = 0; i < arr.length; i++)
        {
            while (arr[i] === ' ')
                i++;
            if (arr[i] in operators)
            {
                if (arr[i] === ')')
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
                    if (arr[i] !== '(') {
                        if (arr[i] === '-' &&
                            (arr.substring(i < 1? 0 : i - 1).search(/^\d-\d/) === -1
                            && arr.substring(i < 1? 0 : i - 1).search(/^ - /) === -1))
                        {

                            var nb = parseFloat(arr.substring(i));
                            stack.push(nb);
                            i += nb.toString().length;
                        }
                        else {
                            var lastOperand = operands[operands.length - 1];

                            while (priority[arr[i]] <= priority[lastOperand]) {
                                stack.push(operands.pop());
                                lastOperand = operands[operands.length - 1];
                            }
                        }
                    }
                    else
                        opened = true;
                    if (arr[i] !== ' ')
                        operands.push(arr[i]);
                }
            }
            else {
                nb = parseFloat(arr.substring(i));
                stack.push(nb);
                i += nb.toString().length - 1;
            }
        }

        if (opened)
            return undefined;
        while (operands.length > 0)
            stack.push(operands.pop());


        var outputStack = [];

        var results = [];
        for(i = 0; i < stack.length; i++) {
            if (stack[i] in operators)
            {
                var n2 = outputStack.pop(), n1 = outputStack.pop();
                outputStack.push(operators[stack[i]](n1, n2));
                results.push(operators[stack[i]](n1, n2));
            }
            else {
                outputStack.push(stack[i]);
            }
        }

        if (outputStack.length > 1)
            return undefined;
        else
            return +outputStack.pop().toFixed(2);
    }
};

module.exports = eval;