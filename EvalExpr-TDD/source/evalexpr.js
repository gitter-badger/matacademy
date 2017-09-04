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
        }

        var operands = [];
        var stack = [];
        var opened = false;

        if (str.search(/^[\d\s.+\-()\/*]+$/) === -1)
             return undefined;
        if (str.search(/[+\-\/*][+\-\/*]+/) !== -1)
            return undefined;
        var arr = str.replace(/[\s]/g, "");
        for(var i = 0; i < arr.length; i++)
        {
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
                        var lastOperand = operands[operands.length - 1];

                        while (priority[arr[i]] <= priority[lastOperand]) {
                            stack.push(operands.pop());
                            lastOperand = operands[operands.length - 1];
                        }
                    }
                    else
                        opened = true;
                    operands.push(arr[i]);
                }
            }
            else {
                var nb = parseFloat(arr.substring(i));

                stack.push(nb);
                i += nb.toString().length - 1;
            }
        }
        if (opened)
            return undefined;
        while (operands.length > 0)
            stack.push(operands.pop());


        //return stack + operands;

        var outputStack = [];

        for(i = 0; i < stack.length; i++) {
            if (stack[i] in operators)
            {
                var n2 = parseFloat(outputStack.pop()), n1 = parseFloat(outputStack.pop());
                outputStack.push(operators[stack[i]](n1, n2));
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