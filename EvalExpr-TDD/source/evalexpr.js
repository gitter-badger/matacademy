var eval = {
    evaluateExpression: function (str) {
        var operators = {
                '+': function(x, y) {return x + y},
            '-': function(x, y) {return x - y},
            '*': function(x, y) {return x * y},
            '/': function(x, y) {return x / y},
            ')': function () {return;},
            '(': function () {return;}
    };

        var operands = [];
        var stack = [];

        //var arr = str.split("");

        var arr = str;
        for(var i = 0; i < arr.length; i++)
        {
            if (arr[i] === ' ')
                i++;
            if (arr[i] in operators)
            {
                if (arr[i] === ')')
                {
                    var op = operands.pop();
                    while (op !== '(')
                    {
                        stack.push(op);
                        op = operands.pop();
                    }
                }
                else {
                 operands.push(arr[i]);
                }
            }
            else {
                var nb = parseFloat(arr.substring(i));
                stack.push(nb);
                i += nb.toString().length - 1;
            }
        }
        operands.forEach(function (t) { stack.push(t); });

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
            return "error";
        else
            return +outputStack.pop().toFixed(2);
    }
};

module.exports = eval;