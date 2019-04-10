app.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {

            scope.isLoading = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isLoading, function (v) {
                if (v) {
                    elm.show();
                } else {
                    elm.hide();
                }
            });
        }
    };

}]);

app.directive('myEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });
                event.preventDefault();
            }
        });
    };
});

app.directive('buttonPrint', function () {
    var link = function ($scope, element, attributes) {
        $scope.printWindow1 = function () {
            $(".toplinks").hide();
            $("#header").hide();
            $("#navigation").hide();
            $("#footer").hide();
            window.print();
            $(".toplinks").show();
            $("#header").show();
            $("#navigation").show();
            $("#footer").show();
        };
    };
    return {
        restrict: 'E',
        link: link,
        template: "<li style='float: right'><a class='print cursor-pointer' data-toggle='tooltip' title='Print' ng-click='printWindow1();'></a></li>",
        replace: true
    };
});

app.directive('modalPrint', function () {
    var link = function ($scope, element, attributes) {
        $scope.printWindow = function (event) {
            var id = angular.element(event.target).parent('li').attr('data-id');
            $(".top_msg").hide();
            $(".modal-header").hide();
            $(".toplinks").hide();
            $(".text-label").hide();
            var divToPrint = $("#" + id + "");
            var popupWin = window.open('', '_blank', 'width=500,height=500');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.html() + '</html>');
            $(".toplinks").show();
            $(".modal-header").show();
            $(".top_msg").show();
            $(".text-label").show();
            popupWin.document.close();
        };
    };
    return {
        restrict: 'E',
        link: link,
        template: "<li style='float: right'><a class='print cursor-pointer' data-toggle='tooltip' title='Print' ng-click='printWindow($event);'></a></li>",
        replace: true
    };
});

app.directive('buttonSave', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnSave' ng-src='../../Images/buttons/buttonSave.gif' alt='Save' ng-click='ActionSave();' /><br><span class='GrayColorText fontTextSize'>Save</span></div>",
        replace: true
    };
});

app.directive('buttonEdit', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnEdit' ng-src='../../Images/buttons/buttonEdit.gif' alt='Edit' ng-click='ActionEdit();' /><br><span class='GrayColorText fontTextSize'>Edit</span></div>",
        replace: true
    };
});

app.directive('buttonAdd', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnAdd' ng-src='../../Images/buttons/buttonadd.gif' alt='Add' ng-click='ActionAdd();' /><br><span class='GrayColorText fontTextSize'>Add</span></div>",
        replace: true
    };
});

app.directive('buttonCalculate', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnCalculate' ng-src='../../Images/buttons/buttonCalculate.png' alt='Calculate' ng-click='ActionCalculate();' /><br><span class='GrayColorText fontTextSize'>Calculate</span></div>",
        replace: true
    };
});

app.directive('buttonPrevious', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnPrevious' ng-src='../../Images/buttons/buttonPrevious.gif' alt='Previous' ng-click='ActionPrivious();' /><br><span class='GrayColorText fontTextSize'>Previous</span></div>",
        replace: true
    };
});

app.directive('buttonNext', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnNext' ng-src='../../Images/buttons/buttonNext.gif' alt='Next' ng-click='ActionNext();' /><br><span class='GrayColorText fontTextSize'>Next</span></div>",
        replace: true
    };
});

app.directive('buttonFinish', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnFinish' ng-src='../../Images/buttons/buttonFinish.gif' alt='Finish' ng-click='ActionFinish();' /><br><span class='GrayColorText fontTextSize'>Finish</span></div>",
        replace: true
    };
});

app.directive('buttonSubmit', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnSubmit' ng-src='../../Images/buttons/buttonSubmit.gif' alt='Submit' ng-click='ActionSubmit();' /><br><span class='GrayColorText fontTextSize'>Submit</span></div>",
        replace: true
    };
});

app.directive('buttonReset', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnReset' ng-src='../../Images/buttons/buttonReset.gif' alt='Reset' ng-click='ActionReset();' /><br><span class='GrayColorText fontTextSize'>Reset</span></div>",
        replace: true
    };
});

app.directive('buttonRun', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnRun' ng-src='../../Images/buttons/buttonRun.gif' alt='Run' ng-click='ActionRun();' /><br><span class='GrayColorText fontTextSize'>Run</span></div>",
        replace: true
    };
});

app.directive('buttonOptions', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnOptions' ng-src='../../Images/buttons/buttonOptions.png' alt='Options' ng-click='ActionOptions();' /><br><span class='GrayColorText fontTextSize'>Options</span></div>",
        replace: true
    };
});

app.directive('buttonWorkflow', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnWorkflow' ng-src='../../Images/buttons/buttonWorkflow.png' alt='Workflow' ng-click='ActionWorkflow();' /><br><span class='GrayColorText fontTextSize'>Workflow</span></div>",
        replace: true
    };
});

app.directive('buttonViewdoc', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnViewdoc' ng-src='../../Images/buttons/buttonviewdoc.png' alt='ViewDoc' ng-click='ActionViewdoc();' /><br><span class='GrayColorText fontTextSize'>ViewDoc</span></div>",
        replace: true
    };
});

app.directive('buttonCanceldoc', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnCanceldoc' ng-src='../../Images/buttons/buttoncanceldoc.png' alt='CancelDoc' ng-click='ActionCancelDoc();' /><br><span class='GrayColorText fontTextSize'>CancelDoc</span></div>",
        replace: true
    };
});

app.directive('buttonPdf', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnPDF' ng-src='../../Images/buttons/buttonPDF.png' alt='PDF' ng-click='ActionGenratePDF();' /><br><span class='GrayColorText fontTextSize'>PDF</span></div>",
        replace: true
    };
});

app.directive('buttonSubmitdoc', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnSubmitDoc' ng-src='../../Images/buttons/buttonSubmitDoc.png' alt='SubmitDoc' ng-click='ActionSubmitDoc();' /><br><span class='GrayColorText fontTextSize'>SubmitDoc</span></div>",
        replace: true
    };
});

app.directive('buttonUpload', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnUpload' ng-src='../../Images/buttons/buttonUpload.png' alt='Upload' ng-click='ActionUpload();' /><br><span class='GrayColorText fontTextSize'>Upload</span></div>",
        replace: true
    };
});

app.directive('buttonDownload', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border cursor-pointer'><img id='btnDownload' ng-src='../../Images/buttons/buttonDownload.png' alt='Download' ng-click='ActionDownload();'/><br><span class='GrayColorText fontTextSize'>Download</span></div>",
        replace: true
    };
});

app.directive('buttonDivider', function () {
    return {
        restrict: 'E',
        template: "<div class='btnFloatLeft no-border'><img id='btnDivider' ng-src='../../Images/buttons/icon_toolbar_divider.gif' alt='Divider'/><br></div>",
        replace: true
    };
});

app.directive('buttonHelp', function () {
    return {
        restrict: 'EA',
        template: "<li ng-controller='helpController' style='float: right'>" +
            "<a class='help cursor-pointer' data-toggle='tooltip' title='Help' ng-click='getHelpPopUp(resourceType)'></a>" +
            "</li>",
        link: function (scope, elem, attr) {
            scope.resourceType = attr.resourceType;
        },
        replace: true
    };
});

app.directive('buttonHelpField', function () {
    return {
        restrict: 'EA',
        // template: "<img class='helplink' src='Content/images/helpimg.png' ng-controller='helpController' style='cursor:pointer' ng-click='getHelpPopUp(resourceType)' />",
        template: "<a href='' ng-controller='helpController' data-ng-click='getHelpPopUp(resourceType)' style='cursor:pointer' class='helplink'></a>",
        link: function (scope, elem, attr) {
            scope.resourceType = attr.resourceType;
        },
        replace: true
    };
});

app.directive('closeNotification', function () {
    return function (scope, element) {
        element.bind('click', function () {
            angular.element(element).parent('.alert').hide();
        });
    };
});

app.directive('customOnChange', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var onChangeHandler = scope.$eval(attrs.customOnChange);
            element.bind('change', onChangeHandler);
        }
    };
});

app.directive('validNumber', function ($parse) {

    return function (scope, element, attrs) {

        element.bind("keypress", function (event) {

            scope.$apply(function () {
                if (isNaN(element.val())) {
                    element.val(null);
                    event.preventDefault();
                }
            });
        });
    }
});

app.directive('dropdownMultiselect', function () {
    return {
        restrict: 'E',
        scope: {
            model: '=',
            options: '=',
            selected: '='
        },
        template:
                "<div class='btn-group multiselectDropdown' data-ng-class='{open: open}'>" +
                    "<button class='btn btn-small' data-ng-click='openDropdown()' type='button'>Select...</button>" +
                    "<button class='btn btn-smalldropdown-toggle'  type='button' data-ng-click='openDropdown()'><span class='caret'></span></button>" +
    "<ul class='dropdown-menu' aria-labelledby='dropdownMenu' style='max-height: 300px;overflow: scroll;'>" + "<li><a data-ng-click='selectAll()'><span class='glyphicon glyphicon-ok green' aria-hidden='true'></span> Check All</a></li>" +
    "<li><a data-ng-click='deselectAll();'><span class='glyphicon glyphicon-remove red' aria-hidden='true'></span> Uncheck All</a></li>" +
    "<li class='divider'></li>" + "<li data-ng-repeat='option in options'><a data-ng-click='toggleSelectItem(option)'><span data-ng-class='getClassName(option)' aria-hidden='true'></span> {{option.name}}</a></li>" +
    "</ul>" +
"</div><strong class='margin-left-5'>{{selected}} Selected</strong>",
        controller: function ($scope) {

            $scope.openDropdown = function () {
                $scope.open = !$scope.open;
            };

            $scope.selectAll = function () {
                $scope.model = [];
                angular.forEach($scope.options, function (item, index) {
                    $scope.model.push(item.id);
                });
            };

            $scope.deselectAll = function () {
                $scope.model = [];
            };

            $scope.toggleSelectItem = function (option) {

                var intIndex = -1;
                angular.forEach($scope.model, function (item, index) {
                    if (item == option.id) {
                        intIndex = index;
                    }
                });

                if (intIndex >= 0) {
                    $scope.model.splice(intIndex, 1);
                }
                else {
                    $scope.model.push(option.id);
                }
            };

            $scope.getClassName = function (option) {

                var varClassName = 'glyphicon glyphicon-remove';
                angular.forEach($scope.model, function (item, index) {
                    if (item == option.id) {
                        varClassName = 'glyphicon glyphicon-ok green';
                    }
                });
                return (varClassName);
            };
        }
    }
});

// Box Hierarchy

app.directive('frangTree', function ($parse, $animate) {
        return {
            restrict: 'EA',
            controller: function ($scope, $element) {
                this.insertChildren = null;
                this.init = function (insertChildren) {
                    this.insertChildren = insertChildren;
                };
            }
        };
    })

app.directive('frangTreeRepeat', function ($parse, $animate) {

        // ---------- Some necessary internal functions from angular.js ----------

        function hashKey(obj) {
            var objType = typeof obj,
                key;

            if (objType == 'object' && obj !== null) {
                if (typeof (key = obj.$$hashKey) == 'function') {
                    // must invoke on object to keep the right this
                    key = obj.$$hashKey();
                } else if (key === undefined) {
                    key = obj.$$hashKey = nextUid();
                }
            } else {
                key = obj;
            }

            return objType + ':' + key;
        }
        function isArrayLike(obj) {
            if (obj == null || isWindow(obj)) {
                return false;
            }

            var length = obj.length;

            if (obj.nodeType === 1 && length) {
                return true;
            }

            return isString(obj) || isArray(obj) || length === 0 ||
                typeof length === 'number' && length > 0 && (length - 1) in obj;
        }
        function isWindow(obj) {
            return obj && obj.document && obj.location && obj.alert && obj.setInterval;
        }
        function isString(value) { return typeof value == 'string'; }
        function isArray(value) {
            return toString.apply(value) == '[object Array]';
        }
        var uid = ['0', '0', '0'];
        function nextUid() {
            var index = uid.length;
            var digit;

            while (index) {
                index--;
                digit = uid[index].charCodeAt(0);
                if (digit == 57 /*'9'*/) {
                    uid[index] = 'A';
                    return uid.join('');
                }
                if (digit == 90  /*'Z'*/) {
                    uid[index] = '0';
                } else {
                    uid[index] = String.fromCharCode(digit + 1);
                    return uid.join('');
                }
            }
            uid.unshift('0');
            return uid.join('');
        }
        function assertNotHasOwnProperty(name, context) {
            if (name === 'hasOwnProperty') {
                throw ngMinErr('badname', "hasOwnProperty is not a valid {0} name", context);
            }
        }
        var jqLite = angular.element;
        var forEach = angular.forEach;

        function minErr(module) {
            return function () {
                var code = arguments[0],
                    prefix = '[' + (module ? module + ':' : '') + code + '] ',
                    template = arguments[1],
                    templateArgs = arguments,
                    stringify = function (obj) {
                        if (isFunction(obj)) {
                            return obj.toString().replace(/ \{[\s\S]*$/, '');
                        } else if (isUndefined(obj)) {
                            return 'undefined';
                        } else if (!isString(obj)) {
                            return JSON.stringify(obj);
                        }
                        return obj;
                    },
                    message, i;

                message = prefix + template.replace(/\{\d+\}/g, function (match) {
                    var index = +match.slice(1, -1), arg;

                    if (index + 2 < templateArgs.length) {
                        arg = templateArgs[index + 2];
                        if (isFunction(arg)) {
                            return arg.toString().replace(/ ?\{[\s\S]*$/, '');
                        } else if (isUndefined(arg)) {
                            return 'undefined';
                        } else if (!isString(arg)) {
                            return toJson(arg);
                        }
                        return arg;
                    }
                    return match;
                });

                message = message + '\nhttp://errors.angularjs.org/' + version.full + '/' +
                    (module ? module + '/' : '') + code;
                for (i = 2; i < arguments.length; i++) {
                    message = message + (i == 2 ? '?' : '&') + 'p' + (i - 2) + '=' +
                        encodeURIComponent(stringify(arguments[i]));
                }

                return new Error(message);
            };
        }


        // ---------- Some initializations at the beginning of ngRepeat factory ----------

        var NG_REMOVED = '$$NG_REMOVED';
        var ngRepeatMinErr = minErr('ngRepeat');
        var ngMinErr = minErr('ng');
        var toString = Object.prototype.toString;
        var isFunction = angular.isFunction;
        var isUndefined = angular.isUndefined;
        var toJson = angular.toJson;

        // ---------- Internal function at the end of ngRepeat factory ----------

        function getBlockElements(block) {
            if (block.startNode === block.endNode) {
                return jqLite(block.startNode);
            }

            var element = block.startNode;
            var elements = [element];

            do {
                element = element.nextSibling;
                if (!element) break;
                elements.push(element);
            } while (element !== block.endNode);

            return jqLite(elements);
        }


        // ---------- Add watch, extracted into a function to call it not only on the element but also on its children ----------

        function addRepeatWatch($scope, $element, _lastBlockMap, valueIdentifier, keyIdentifier,
                                rhs, trackByIdExpFn, trackByIdArrayFn, trackByIdObjFn, linker, expression) {
            var lastBlockMap = _lastBlockMap;

            //watch props
            $scope.$watchCollection(rhs, function ngRepeatAction(collection) {
                var index, length,
                    previousNode = $element[0],     // current position of the node
                    nextNode,
                // Same as lastBlockMap but it has the current state. It will become the
                // lastBlockMap on the next iteration.
                    nextBlockMap = {},
                    arrayLength,
                    childScope,
                    key, value, // key/value of iteration
                    trackById,
                    trackByIdFn,
                    collectionKeys,
                    block,       // last object information {scope, element, id}
                    nextBlockOrder = [],
                    elementsToRemove;


                if (isArrayLike(collection)) {
                    collectionKeys = collection;
                    trackByIdFn = trackByIdExpFn || trackByIdArrayFn;
                } else {
                    trackByIdFn = trackByIdExpFn || trackByIdObjFn;
                    // if object, extract keys, sort them and use to determine order of iteration over obj props
                    collectionKeys = [];
                    for (key in collection) {
                        if (collection.hasOwnProperty(key) && key.charAt(0) != '$') {
                            collectionKeys.push(key);
                        }
                    }
                    collectionKeys.sort();
                }

                arrayLength = collectionKeys.length;

                // locate existing items
                length = nextBlockOrder.length = collectionKeys.length;
                for (index = 0; index < length; index++) {
                    key = (collection === collectionKeys) ? index : collectionKeys[index];
                    value = collection[key];
                    trackById = trackByIdFn(key, value, index);
                    assertNotHasOwnProperty(trackById, '`track by` id');
                    if (lastBlockMap.hasOwnProperty(trackById)) {
                        block = lastBlockMap[trackById]
                        delete lastBlockMap[trackById];
                        nextBlockMap[trackById] = block;
                        nextBlockOrder[index] = block;
                    } else if (nextBlockMap.hasOwnProperty(trackById)) {
                        // restore lastBlockMap
                        forEach(nextBlockOrder, function (block) {
                            if (block && block.startNode) lastBlockMap[block.id] = block;
                        });
                        // This is a duplicate and we need to throw an error
                        throw ngRepeatMinErr('dupes', "Duplicates in a repeater are not allowed. Use 'track by' expression to specify unique keys. Repeater: {0}, Duplicate key: {1}",
                            expression, trackById);
                    } else {
                        // new never before seen block
                        nextBlockOrder[index] = { id: trackById };
                        nextBlockMap[trackById] = false;
                    }
                }

                // remove existing items
                for (key in lastBlockMap) {
                    // lastBlockMap is our own object so we don't need to use special hasOwnPropertyFn
                    if (lastBlockMap.hasOwnProperty(key)) {
                        block = lastBlockMap[key];
                        elementsToRemove = getBlockElements(block);
                        $animate.leave(elementsToRemove);
                        forEach(elementsToRemove, function (element) { element[NG_REMOVED] = true; });
                        block.scope.$destroy();
                    }
                }

                // we are not using forEach for perf reasons (trying to avoid #call)
                for (index = 0, length = collectionKeys.length; index < length; index++) {
                    key = (collection === collectionKeys) ? index : collectionKeys[index];
                    value = collection[key];
                    block = nextBlockOrder[index];
                    if (nextBlockOrder[index - 1]) previousNode = nextBlockOrder[index - 1].endNode;

                    if (block.startNode) {
                        // if we have already seen this object, then we need to reuse the
                        // associated scope/element
                        childScope = block.scope;

                        nextNode = previousNode;
                        do {
                            nextNode = nextNode.nextSibling;
                        } while (nextNode && nextNode[NG_REMOVED]);

                        if (block.startNode == nextNode) {
                            // do nothing
                        } else {
                            // existing item which got moved
                            $animate.move(getBlockElements(block), null, jqLite(previousNode));
                        }
                        previousNode = block.endNode;
                    } else {
                        // new item which we don't know about
                        childScope = $scope.$new();
                    }

                    childScope[valueIdentifier] = value;
                    if (keyIdentifier) childScope[keyIdentifier] = key;
                    childScope.$index = index;
                    childScope.$first = (index === 0);
                    childScope.$last = (index === (arrayLength - 1));
                    childScope.$middle = !(childScope.$first || childScope.$last);
                    childScope.$odd = !(childScope.$even = index % 2 == 0);

                    if (!block.startNode) {
                        linker(childScope, function (clone) {
                            clone[clone.length++] = document.createComment(' end ngRepeat: ' + expression + ' ');
                            $animate.enter(clone, null, jqLite(previousNode));
                            previousNode = clone;
                            block.scope = childScope;
                            block.startNode = previousNode && previousNode.endNode ? previousNode.endNode : clone[0];
                            block.endNode = clone[clone.length - 1];
                            nextBlockMap[block.id] = block;
                        });
                    }
                }
                lastBlockMap = nextBlockMap;
            });
        }


        return {
            restrict: 'A',
            transclude: 'element',
            priority: 1000,
            terminal: true,
            require: '^frangTree',
            compile: function (element, attr, linker) {
                return function ($scope, $element, $attr, ctrl) {
                    var expression = $attr.frangTreeRepeat;
                    var match = expression.match(/^\s*(.+)\s+in\s+(.*?)\s*(\s+track\s+by\s+(.+)\s*)?$/),
                        trackByExp, trackByExpGetter, trackByIdExpFn, trackByIdArrayFn, trackByIdObjFn,
                        lhs, rhs, valueIdentifier, keyIdentifier,
                        hashFnLocals = { $id: hashKey };

                    if (!match) {
                        throw ngRepeatMinErr('iexp', "Expected expression in form of '_item_ in _collection_[ track by _id_]' but got '{0}'.",
                            expression);
                    }

                    lhs = match[1];
                    rhs = match[2];
                    trackByExp = match[4];

                    if (trackByExp) {
                        trackByExpGetter = $parse(trackByExp);
                        trackByIdExpFn = function (key, value, index) {
                            // assign key, value, and $index to the locals so that they can be used in hash functions
                            if (keyIdentifier) hashFnLocals[keyIdentifier] = key;
                            hashFnLocals[valueIdentifier] = value;
                            hashFnLocals.$index = index;
                            return trackByExpGetter($scope, hashFnLocals);
                        };
                    } else {
                        trackByIdArrayFn = function (key, value) {
                            return hashKey(value);
                        }
                        trackByIdObjFn = function (key) {
                            return key;
                        }
                    }

                    match = lhs.match(/^(?:([\$\w]+)|\(([\$\w]+)\s*,\s*([\$\w]+)\))$/);
                    if (!match) {
                        throw ngRepeatMinErr('iidexp', "'_item_' in '_item_ in _collection_' should be an identifier or '(_key_, _value_)' expression, but got '{0}'.",
                            lhs);
                    }
                    valueIdentifier = match[3] || match[1];
                    keyIdentifier = match[2];

                    // Store a list of elements from previous run. This is a hash where key is the item from the
                    // iterator, and the value is objects with following properties.
                    //   - scope: bound scope
                    //   - element: previous element.
                    //   - index: position
                    var lastBlockMap = {};


                    addRepeatWatch($scope, $element, /*lastBlockMap*/ {}, valueIdentifier, keyIdentifier,
                        rhs, trackByIdExpFn, trackByIdArrayFn, trackByIdObjFn, linker, expression);

                    ctrl.init(function ($scope, $element, collection) {
                        addRepeatWatch($scope, $element, /*lastBlockMap*/ {}, valueIdentifier, keyIdentifier,
                            collection, trackByIdExpFn, trackByIdArrayFn, trackByIdObjFn, linker, expression)
                    });
                };

            }
        };
    })

app.directive('frangTreeInsertChildren', function () {
        return {
            restrict: 'EA',
            require: '^frangTree',
            link: function (scope, element, attrs, ctrl) {
                var comment = document.createComment('treeRepeat');
                element.append(comment);

                ctrl.insertChildren(scope, angular.element(comment), attrs.frangTreeInsertChildren);
            }
        };
    })

app.directive('frangTreeDrag', function ($parse) {
        return {
            restrict: 'A',
            require: '^frangTree',
            link: function (scope, element, attrs, ctrl) {
                var el = element[0];
                var parsedDrag = $parse(attrs.frangTreeDrag);
                el.draggable = true;
                el.addEventListener(
                    'dragstart',
                    function (e) {
                        if (e.stopPropagation) e.stopPropagation();
                        e.dataTransfer.effectAllowed = 'move';
                        e.dataTransfer.setData('Text', 'nothing'); // Firefox requires some data
                        element.addClass('tree-drag');
                        ctrl.dragData = parsedDrag(scope);
                        return false;
                    },
                    false
                );
                el.addEventListener(
                    'dragend',
                    function (e) {
                        if (e.stopPropagation) e.stopPropagation();
                        element.removeClass('tree-drag');
                        ctrl.dragData = null;
                        return false;
                    },
                    false
                );
            }
        };
    })

app.directive('frangTreeDrop', function ($parse) {
        return {
            restrict: 'A',
            require: '^frangTree',
            link: function (scope, element, attrs, ctrl) {
                var el = element[0];
                var parsedDrop = $parse(attrs.frangTreeDrop);
                var parsedAllowDrop = $parse(attrs.frangTreeAllowDrop || 'true');
                el.addEventListener(
                    'dragover',
                    function (e) {
                        if (parsedAllowDrop(scope, { dragData: ctrl.dragData })) {
                            if (e.stopPropagation) { e.stopPropagation(); }
                            e.dataTransfer.dropEffect = 'move';
                            element.addClass('tree-drag-over');
                            // allow drop
                            if (e.preventDefault) { e.preventDefault(); }
                        }
                        return false;
                    },
                    false
                );
                el.addEventListener(
                    'dragenter',
                    function (e) {
                        if (parsedAllowDrop(scope, { dragData: ctrl.dragData })) {
                            if (e.stopPropagation) { e.stopPropagation(); }
                            element.addClass('tree-drag-over');
                            // allow drop
                            if (e.preventDefault) { e.preventDefault(); }
                        }
                        return false;
                    },
                    false
                );
                el.addEventListener(
                    'dragleave',
                    function (e) {
                        if (parsedAllowDrop(scope, { dragData: ctrl.dragData })) {
                            if (e.stopPropagation) { e.stopPropagation(); }
                            element.removeClass('tree-drag-over');
                        }
                        return false;
                    },
                    false
                );
                el.addEventListener(
                    'drop',
                    function (e) {
                        if (parsedAllowDrop(scope, { dragData: ctrl.dragData })) {
                            if (e.stopPropagation) { e.stopPropagation(); }
                            element.removeClass('tree-drag-over');
                            scope.$apply(function () {
                                parsedDrop(scope, { dragData: ctrl.dragData });
                            });
                            ctrl.dragData = null;
                            if (e.preventDefault) { e.preventDefault(); }
                        }
                        return false;
                    },
                    false
                );
            }
        }
    });


app.directive('noDirty', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            ngModelCtrl.$setDirty = angular.noop;
        }
    }
})

app.directive('ngJsonExportExcel', function () {
            return {
                restrict: 'AE',
                scope: {
                    data: '=',
                    filename: '=?',
                    reportFields: '=',
                    separator: '@'
                },
                link: function (scope, element) {
                    scope.filename = !!scope.filename ? scope.filename : 'export-excel';

                    var fields = [];
                    var header = [];
                    var separator = scope.separator || ';';

                    angular.forEach(scope.reportFields, function (field, key) {
                        if (!field || !key) {
                            throw new Error('error json report fields');
                        }

                        fields.push(key);
                        header.push(field);
                    });

                    element.bind('click', function () {
                        var bodyData = _bodyData();
                        var strData = _convertToExcel(bodyData);

                        var blob = new Blob([strData], { type: "text/plain;charset=utf-8" });

                        return saveAs(blob, [scope.filename + '.csv']);
                    });

                    function _bodyData() {
                        var data = scope.data;
                        var body = "";
                        angular.forEach(data, function (dataItem) {
                            var rowItems = [];

                            angular.forEach(fields, function (field) {
                                if (field.indexOf('.')) {
                                    field = field.split(".");
                                    var curItem = dataItem;

                                    // deep access to obect property
                                    angular.forEach(field, function (prop) {
                                        if (curItem !== null && curItem !== undefined) {
                                            curItem = curItem[prop];
                                        }
                                    });

                                    data = curItem;
                                }
                                else {
                                    data = dataItem[field];
                                }

                                var fieldValue = data !== null ? data : ' ';

                                if (fieldValue !== undefined && angular.isObject(fieldValue)) {
                                    fieldValue = _objectToString(fieldValue);
                                }

                                if (typeof fieldValue == 'string') {
                                    rowItems.push('"' + fieldValue.replace(/"/g, '""') + '"');
                                } else {
                                    rowItems.push(fieldValue);
                                }
                            });

                            body += rowItems.join(separator) + '\n';
                        });

                        return body;
                    }

                    function _convertToExcel(body) {
                        return header.join(separator) + '\n' + body;
                    }

                    function _objectToString(object) {
                        var output = '';
                        angular.forEach(object, function (value, key) {
                            output += key + ':' + value + ' ';
                        });

                        return '"' + output + '"';
                    }
                }
            };
});


app.directive('date', function (dateFilter) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {

            var dateFormat = attrs['date'] || 'MM/dd/yyyy';

            ctrl.$formatters.unshift(function (modelValue) {
                return dateFilter(modelValue, dateFormat);
            });
        }
    };
});




app.directive('fixedHeader', ['$timeout', function ($timeout) {
        return {
            restrict: 'A',
            scope: {
                tableHeight: '@'
            },
            link: function ($scope, $elem, $attrs, $ctrl) {
                function isVisible(el) {
                    var style = window.getComputedStyle(el);
                    return (style.display != 'none' && el.offsetWidth != 0);
                }

                function isTableReady() {
                    return isVisible(elem.querySelector("tbody")) && elem.querySelector('tbody tr:first-child') != null;
                }

                var elem = $elem[0];
                // wait for content to load into table and to have at least one row, tdElems could be empty at the time of execution if td are created asynchronously (eg ng-repeat with promise)
                var unbindWatch = $scope.$watch(isTableReady,
                    function (newValue, oldValue) {
                        if (newValue === true) {
                            // reset display styles so column widths are correct when measured below
                            angular.element(elem.querySelectorAll('thead, tbody, tfoot')).css('display', '')

                            // wrap in $timeout to give table a chance to finish rendering
                            $timeout(function () {
                                // set widths of columns
                                angular.forEach(elem.querySelectorAll('tr:first-child th'), function (thElem, i) {

                                    var tdElems = elem.querySelector('tbody tr:first-child td:nth-child(' + (i + 1) + ')');
                                    var tfElems = elem.querySelector('tfoot tr:first-child td:nth-child(' + (i + 1) + ')');


                                    var columnWidth = tdElems ? tdElems.offsetWidth : thElem.offsetWidth;
                                    if (tdElems) {
                                        tdElems.style.width = columnWidth + 'px';
                                    }
                                    if (thElem) {
                                        thElem.style.width = columnWidth + 'px';
                                    }
                                    if (tfElems) {
                                        tfElems.style.width = columnWidth + 'px';
                                    }
                                });

                                // set css styles on thead and tbody
                                angular.element(elem.querySelectorAll('thead, tfoot')).css('display', 'block')

                                angular.element(elem.querySelectorAll('tbody')).css({
                                    'display': 'block',
                                    'height': $scope.tableHeight || 'inherit',
                                    'overflow': 'auto'
                                });


                                // reduce width of last column by width of scrollbar
                                var tbody = elem.querySelector('tbody');
                                var scrollBarWidth = tbody.offsetWidth - tbody.clientWidth;
                                if (scrollBarWidth > 0) {
                                    // for some reason trimming the width by 2px lines everything up better
                                    scrollBarWidth -= 2;
                                    var lastColumn = elem.querySelector('tbody tr:first-child td:last-child');
                                    lastColumn.style.width = (lastColumn.offsetWidth - scrollBarWidth) + 'px';
                                }
                            });

                            //we only need to watch once
                            unbindWatch();
                        }
                    });
            }
        };
}]);



app.directive('staticInclude', function ($http, $templateCache, $compile) {
    return function (scope, element, attrs) {
        var templatePath = attrs.staticInclude;
        //$http.get(templatePath + "?v=" + ApplicationVersion, { cache: $templateCache }).success(function (response) {
        $http.get(templatePath + "?v=" + ApplicationVersion, { cache: false }).success(function (response) {
            var contents = element.html(response).contents();
            $compile(contents)(scope);
        });
    };
});

app.directive('validateEmail', function () {
    //var EMAIL_REGEXP = /^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
    var EMAIL_REGEXP = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
    
    return {
        require: 'ngModel',
        restrict: '',
        link: function (scope, elm, attrs, ctrl) {
            // only apply the validator if ngModel is present and Angular has added the email validator
            if (ctrl && ctrl.$validators.email) {

                // this will overwrite the default Angular email validator
                ctrl.$validators.email = function (modelValue) {
                    return ctrl.$isEmpty(modelValue) || EMAIL_REGEXP.test(modelValue);
                };
            }
        }
    };
});

app.directive("passwordVerify", function () {
    return {
        require: "ngModel",
        scope: {
            passwordVerify: '='
        },
        link: function (scope, element, attrs, ctrl) {
            scope.$watch(function () {
                var combined;

                if (scope.passwordVerify || ctrl.$viewValue) {
                    combined = scope.passwordVerify + '_' + ctrl.$viewValue;
                }
                return combined;
            }, function (value) {
                if (value) {
                    ctrl.$parsers.unshift(function (viewValue) {
                        var origin = scope.passwordVerify;
                        if (origin !== viewValue) {
                            ctrl.$setValidity("passwordVerify", false);
                            return undefined;
                        } else {
                            ctrl.$setValidity("passwordVerify", true);
                            return viewValue;
                        }
                    });
                }
            });
        }
    };
});


app.directive('onFinishRender', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit(attr.onFinishRender);
                });
            }
        }
    }
});

app.directive('bxSlider', [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.$on('repeatFinished', function () {                
                element.bxSlider(scope.$eval('{' + attrs.bxSlider + '}'));
            });
        }
    }
}])
app.directive('notifyWhenRepeatFinished', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last === true) {
                $timeout(function () {
                    scope.$emit('repeatFinished');
                });
            }
        }
    }
}]);
app.directive("htmlStyle", function () {
    return {
        restrict: "EA",
        template: "<p></p>",
        scope: {
            text: "@text"
        }
    };
});