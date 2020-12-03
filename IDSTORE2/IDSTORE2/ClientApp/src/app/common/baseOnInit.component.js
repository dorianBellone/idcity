"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.BaseDestroyableComponent = void 0;
/**
 * Base class for all components with subscriptions which have to be unsubscribed on component's destroying
 */
var BaseDestroyableComponent = /** @class */ (function () {
    function BaseDestroyableComponent() {
        this.subscriptions = [];
    }
    BaseDestroyableComponent.prototype.ngOnDestroy = function () {
        this.subscriptions.forEach(function (item) { return item.unsubscribe(); });
    };
    BaseDestroyableComponent.prototype.subscribe = function (observable, next, error, complete) {
        this.subscriptions.push(observable.subscribe(next, error, complete));
    };
    return BaseDestroyableComponent;
}());
exports.BaseDestroyableComponent = BaseDestroyableComponent;
//# sourceMappingURL=baseOnInit.component.js.map