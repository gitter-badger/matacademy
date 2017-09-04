"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
/**
 * Created by atrepyto on 9/2/17.
 */
require("rxjs/add/operator/switchMap");
var core_1 = require("@angular/core");
var task_service_1 = require("./task.service");
var common_1 = require("@angular/common");
var TasksComponent = (function () {
    function TasksComponent(taskService, location) {
        this.taskService = taskService;
        this.location = location;
    }
    TasksComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.taskService.getTasks().then(function (tasks) { return _this.tasks = tasks; });
    };
    TasksComponent.prototype.addTask = function (text) {
        var _this = this;
        text = text.trim();
        if (!text) {
            return;
        }
        this.taskService.createTask(text)
            .then(function (task) {
            _this.tasks.push(task);
        });
    };
    TasksComponent.prototype.deleteTask = function (task) {
        var _this = this;
        this.taskService
            .deleteTask(task.id)
            .then(function () {
            _this.tasks = _this.tasks.filter(function (h) { return h !== task; });
        });
    };
    TasksComponent.prototype.markDone = function (task) {
        task.done = true;
        this.taskService.updateTask(task);
    };
    return TasksComponent;
}());
TasksComponent = __decorate([
    core_1.Component({
        selector: 'tasks',
        templateUrl: './tasks.component.html',
    }),
    __metadata("design:paramtypes", [task_service_1.TaskService,
        common_1.Location])
], TasksComponent);
exports.TasksComponent = TasksComponent;
//# sourceMappingURL=tasks.component.js.map