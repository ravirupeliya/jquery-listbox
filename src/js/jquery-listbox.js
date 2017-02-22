/*
* jquery-listbox - v0.0.1
* Copyright 2017, Ravi Rupeliya
* Email : ravirupeliya@gmail.com
* Free to use under the MIT license.
*/

(function ($) {
    "use strict";
    'namespace chklv';
    $.fn.CheckListView = function (options) {

        var settings = $.extend({
            dataSource: [],
            dataTextField: '',
            dataValueField: '',
            template: '',
            checkedClassName: 'fa-check-square',
            unCheckedClassName: 'fa-square-o',
            maxHeight: '',
            height: '',
            allowMultiSelection: !0
        }, options);

        var NAVIGATION_DIR = {
            UP: 1,
            DOWN: 2
        };

        var ChklvItem = function (dataItem, liEle) {
            this.dataItem = dataItem;
            this.liEle = liEle;
        };

        ChklvItem.prototype.hideItem = function () {
            this.liEle.addClass('hidden');
            this.dataItem.hidden = true;
        };

        ChklvItem.prototype.showItem = function () {
            this.liEle.removeClass('hidden');
            this.dataItem.hidden = false;
        };

        ChklvItem.prototype.enableItem = function () {
            this.liEle.removeClass('disabled');
            this.dataItem.disabled = false;
        };

        ChklvItem.prototype.disableItem = function () {
            this.liEle.addClass('disabled');
            this.dataItem.disabled = true;
        };

        ChklvItem.prototype.selectItem = function () {
            if (!this.dataItem || this.dataItem.disabled) return;
            this.liEle.addClass('selected');
            this.dataItem.selected = true;
            this.liEle.find('i').addClass(settings.checkedClassName);
        };

        ChklvItem.prototype.deSelectItem = function () {
            if (!this.dataItem || this.dataItem.disabled) return;
            this.liEle.removeClass('selected');
            this.dataItem.selected = false;
            this.liEle.find('i').removeClass(settings.checkedClassName);
        };

        var returnValue = this.each(function () {
            var divEle = this;

            if (this.chklv || !$(this).is('div')) return;

            this.chklv = {
                E: $(divEle),
                _createElems: function () {
                    var R = this;

                    R.wrprDiv = $('<div class="CheckListView" tabindex="-1">');

                    R.optsDiv = R._createWrapper();

                    R.ul = R._createUl();;

                    R.ul.append(R._createItems(settings.dataSource));

                    R.optsDiv.append(R.ul);

                    R.wrprDiv.append(R.optsDiv);

                    R.E.append(R.wrprDiv);
                    R._bindChklvEvents();
                },

                _createWrapper: function () {
                    var wrapperDiv = $('<div class="optionsWrapper">');
                    return wrapperDiv;
                },

                _createUl: function () {
                    var ul = $('<ul class="options">');
                    ul.css('overflow-y', 'auto');
                    if (settings.maxHeight != '') {
                        ul.css('max-height', settings.maxHeight);
                    }
                    if (settings.height != '') {
                        ul.css('height', settings.height);
                    }
                    return ul;
                },

                _createItems: function (dataItems, d) {
                    var R = this;
                    var arrLi = [];

                    //var t0 = performance.now();
                    var itemIndex = 0;
                    $(dataItems).each(function (i, dataItem) {
                        if (!dataItem.disabled) dataItem.disabled = !1;
                        if (!dataItem.selected) dataItem.selected = !1;
                        if (!dataItem.hidden) dataItem.hidden = !1;
                        dataItem.itemIndex = itemIndex++;
                        arrLi.push(R._chklvDataBinding(dataItem, d));
                    });

                    //var t1 = performance.now();
                    //console.log("Call to doSomething took " + (t1 - t0) + " milliseconds.")

                    return arrLi;
                },

                _chklvDataBinding: function (dataItem, d) {
                    var R = this;
                    var text = dataItem[settings.dataTextField];
                    if (settings.template != '') {
                        text = R._renderCustomTemplate(settings.template, dataItem);
                    }
                    var li = $('<li class="option" chklvval="' + dataItem[settings.dataValueField] + '"><span><i class="fa ' + settings.unCheckedClassName + '"></i></span><label>' + text + '</label></li>');

                    li.data('ChklvItem', R._getChklvItem(dataItem, li));

                    if (dataItem.disabled || d)
                        li = li.addClass('disabled');

                    if (dataItem.hidden)
                        li = li.addClass('hidden');

                    if (dataItem.selected) {
                        li.addClass('selected');
                        li.find('i').addClass(settings.checkedClassName);
                    }

                    if (dataItem.className)
                        li.addClass(dataItem.className);

                    if (settings.dataBindingHandler)
                        li = settings.dataBindingHandler.apply(dataItem, [li]);

                    return li;
                },

                _renderCustomTemplate: function (tem, dataItem) {
                    var re = /#:([A-z\s]+)#/g;
                    var rv = tem;
                    var m;
                    do {
                        m = re.exec(tem);
                        if (m) {
                            if (m[1].trim() != '') {
                                if (!dataItem.hasOwnProperty(m[1].trim())) throw m[1].trim() + ' is not defined';
                                rv = rv.replace(m[0], dataItem[m[1].trim()]);
                            }
                            else {
                                rv = rv.replace(m[0], '');
                            }
                        }
                    } while (m);
                    return rv;
                },

                _getChklvItem: function (data, li) {
                    return new ChklvItem(data, li);
                },

                _navigate: function (dir) {

                    var R = this;
                    var allOpts = R.ul.find('li.option:not(.disabled, .hidden)');
                    var focusedEle = R.ul.find('li.option.focused:not(.disabled, .hidden)');
                    var idx = allOpts.index(focusedEle);
                    var nextEle;
                    if (!focusedEle.length) {
                        nextEle = allOpts.eq(0);

                        R.optsDiv.find('li.focused').removeClass('focused');
                        nextEle.addClass('focused');

                        var idx = allOpts.index(nextEle);
                    }

                    if (!nextEle) {
                        if (dir === NAVIGATION_DIR.UP && idx > 0)
                            nextEle = allOpts.eq(idx - 1);
                        else if (dir === NAVIGATION_DIR.DOWN && idx < allOpts.length - 1 && idx > -1)
                            nextEle = allOpts.eq(idx + 1);
                        else return;
                    }

                    focusedEle.removeClass('focused');
                    focusedEle = nextEle.addClass('focused');

                    var ulEle = R.ul;
                    var curSt = ulEle.scrollTop();
                    var eleSt = focusedEle.position().top + curSt;
                    if (eleSt >= curSt + ulEle.height() - focusedEle.outerHeight())
                        ulEle.scrollTop(eleSt - ulEle.height() + focusedEle.outerHeight());
                    if (eleSt < curSt)
                        ulEle.scrollTop(eleSt);
                },

                _bindChklvEvents: function () {
                    var R = this;
                    R.wrprDiv.on('click.chklv', 'li', function (evt) {
                        var liEle = $(this);
                        var focusedEle = R.ul.find('li.option.focused:not(.hidden)');
                        focusedEle.removeClass('focused');
                        liEle.addClass('focused');

                        var changed = R._toggleItemSelection(liEle);

                        R._triggerClickEvent(liEle, evt);
                        if (changed) R._triggerChangeEvent(liEle, evt);
                    });
                    R.wrprDiv.on('keydown.chklv', function (evt) {
                        switch (evt.which) {
                            case 38: // up
                                R._navigate(NAVIGATION_DIR.UP);
                                break;
                            case 40: // down
                                R._navigate(NAVIGATION_DIR.DOWN);
                                break;
                            case 32: // space
                            case 13: // enter
                                R.optsDiv.find('ul li.focused').trigger('click');
                                break;
                            default:
                                return;
                        }
                        evt.preventDefault();
                    });
                },

                _triggerClickEvent: function (ele, evt) {
                    if (settings.clickHandler) {
                        var args = [];
                        args.push(evt);
                        settings.clickHandler.apply(ele, args);
                    }
                },

                _triggerChangeEvent: function (ele, evt) {
                    if (settings.changeHandler) {
                        var args = [];
                        args.push(evt);
                        settings.changeHandler.apply(ele, args);
                    }
                },

                _toggleItemSelection: function (ele) {
                    var R = this;
                    var chklvItem = ele.data('ChklvItem');
                    if (chklvItem.dataItem.disabled) return false;
                    if (!settings.allowMultiSelection) {
                        if (chklvItem.dataItem.selected) return false;
                        ele.parent().find('li.selected').each(function (i, e) {
                            $(e).data('ChklvItem').deSelectItem();
                        });
                    }
                    if (chklvItem.dataItem.selected)
                        chklvItem.deSelectItem();
                    else
                        chklvItem.selectItem();
                    return true;
                },

                _changeControlState: function (enable) {
                    var R = this;
                    R.enabled = val;

                    if (val) {
                        R.wrprDiv.addClass('disabled', 'disabled');
                    }
                    else {
                        R.wrprDiv.removeClass('disabled');
                    }

                    return R;
                },

                enableItemByIndex: function (index) {
                    var R = this;
                    var chklvItem;
                    if (typeof (index) === "number") {
                        chklvItem = R.E.find('.option:eq(' + index + ')').data('ChklvItem');
                    }
                    else {
                        throw 'Invalid index value';
                    }

                    if (chklvItem.dataItem.disabled) {
                        chklvItem.enableItem();
                    }
                },

                changeSelectionByIndex: function (selected, index) {
                    if (!settings.allowMultiSelection && !selected) return;

                    var R = this;
                    var chklvItem;
                    if (typeof (index) === "number") {
                        chklvItem = R.E.find('.option:eq(' + index + ')').data('ChklvItem');
                    }
                    else {
                        throw 'Invalid index value';
                    }
                    if (!chklvItem || !chklvItem.dataItem || chklvItem.dataItem.disabled) return;

                    if (chklvItem.dataItem.selected != selected) {
                        if (!settings.allowMultiSelection) {
                            if (chklvItem.liEle.hasClass('selected')) return false;
                            chklvItem.liEle.parent().find('li.selected').each(function (i, e) {
                                $(e).data('ChklvItem').deSelectItem();
                            });
                        }

                        if (selected)
                            chklvItem.selectItem();
                        else
                            chklvItem.deSelectItem();

                        //R._triggerClickEvent(dataItem.liEle);
                        //R._triggerChangeEvent(dataItem.liEle);
                    }
                },

                changeSelectionByValue: function (selected, value) {
                    if (!settings.allowMultiSelection && !selected) return;

                    var R = this;
                    var chklvItem = R.E.find('.option[chklvval="' + value + '"]').data('ChklvItem') || 0;
                    if (!chklvItem || !chklvItem.dataItem || chklvItem.dataItem.disabled) return;

                    if (chklvItem.dataItem.selected != selected) {
                        if (!settings.allowMultiSelection) {
                            if (chklvItem.liEle.hasClass('selected')) return false;
                            chklvItem.liEle.parent().find('li.selected').each(function (i, e) {
                                $(e).data('ChklvItem').deSelectItem();
                            });
                        }

                        if (selected)
                            chklvItem.selectItem();
                        else
                            chklvItem.deSelectItem();

                        //R._triggerClickEvent(dataItem.liEle);
                        //R._triggerChangeEvent(dataItem.liEle);
                    }
                },

                getSelectedChklvItems: function () {
                    var R = this;
                    var selectedItems = [];

                    R.E.find('.option').each(function (i, e) {
                        var chklvItem = $(e).data('ChklvItem');
                        if (chklvItem.dataItem.selected) selectedItems.push(chklvItem);
                    });
                    return selectedItems;
                },

                getAllChklvItems: function () {
                    var R = this;
                    var allItems = [];

                    R.E.find('.option').each(function (i, e) {
                        var chklvItem = $(e).data('ChklvItem');
                        allItems.push(chklvItem);
                    });
                    return allItems;
                },

                reload: function () {
                    var elm = this.unload();
                    return $(elm).CheckListView(settings);
                },

                unload: function () {
                    var R = this;
                    //R.select.before(R.E);
                    R.E.show();

                    R.wrprDiv.remove();
                    delete divEle.chklv;
                    return divEle;
                },

                enabled: true,

                enable: function () { return this._changeControlState(false) },

                disable: function () { return this._changeControlState(true) },

                init: function () {
                    var R = this;
                    R._createElems();
                    return R;
                }
            };

            divEle.chklv.init();
        });

        return returnValue.length == 1 ? returnValue[0] : returnValue;
    };

}(jQuery));