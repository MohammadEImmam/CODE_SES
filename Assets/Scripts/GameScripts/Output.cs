using System;
using System.Collections.Generic;

namespace taskNamespace {
class Output {
    List<object> parameters;
    public object result;

    public Output(List<object> parameters, object result) {
        this.parameters = parameters;
        this.result = result;
    }
}
}