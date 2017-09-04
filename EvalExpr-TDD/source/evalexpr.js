var eval = {
    evaluateExpression: function (str) {
        var operators = ['+', '-', '/', '*', '(', ')'];

        var operands = [];
        var stack = [];

        var arr = str.split("");

        for(var i = 0; i < arr.length; i++)
        {
            if (operators.includes(arr[i]))
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
                stack.push(arr[i]);
            }
        }
        operands.forEach(function (t) { stack.push(t); });

        var outputStack = [];

        for(i = 0; i < stack.length; i++) {
            if (operators.includes(stack[i]))
            {
                var n2 = parseFloat(outputStack.pop()), n1 = parseFloat(outputStack.pop());
                if (stack[i] === '+')
                    outputStack.push(n2 + n1);
                else if (stack[i] === '-')
                    outputStack.push(n2 - n1);
                else if (stack[i] === '*')
                    outputStack.push(n2 * n1);
                else if (stack[i] === '/')
                    outputStack.push(n2 / n1);
            }
            else {
                outputStack.push(stack[i]);
            }
        }

        if (outputStack.length > 1)
            return "error";
        else
            return parseFloat(outputStack.pop());
    }
};

module.exports = eval;