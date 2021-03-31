declare class TypeUtility {
    static IsObject(value: unknown): value is Nullable<object>;
    static IsString(value: unknown): value is string;
    static IsNumber(value: unknown): value is number;
    static IsInteger(value: unknown): value is number;
    static IsBoolean(value: unknown): value is boolean;
    static IsFunction(value: unknown): value is AnyFunction;
    static IsJQuery(value: unknown): value is JQuery;
    static IsUndefined(value: unknown): value is undefined;
    static IsDefined(value: unknown): value is NotUndefined;
    static IsNull(value: unknown): value is null;
    static HasProperty<TTarget, TKey extends string>(target: TTarget, key: TKey): target is TTarget & UndeclaredProperty<TKey, unknown>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "string"): target is TTarget & UndeclaredProperty<TKey, string>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "number"): target is TTarget & UndeclaredProperty<TKey, number>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "bigint"): target is TTarget & UndeclaredProperty<TKey, bigint>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "boolean"): target is TTarget & UndeclaredProperty<TKey, boolean>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "symbol"): target is TTarget & UndeclaredProperty<TKey, symbol>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "undefined"): target is TTarget & UndeclaredProperty<TKey, undefined>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "object"): target is TTarget & UndeclaredProperty<TKey, object>;
    static HasPropertyOfType<TTarget, TKey extends string>(target: TTarget, key: TKey, type?: "function"): target is TTarget & UndeclaredProperty<TKey, Function>;
}
declare class StringUtility {
    static IsNullOrEmpty(value: Nullable<string>): value is null | "";
}
declare class ArgumentUtility {
    static CheckNotNull(name: string, value: unknown): asserts value is NotNull;
    static CheckTypeIsString(name: string, value: unknown): asserts value is Nullable<string>;
    static CheckNotNullAndTypeIsString(name: string, value: unknown): asserts value is string;
    static CheckTypeIsObject(name: string, value: unknown): asserts value is Nullable<object>;
    static CheckNotNullAndTypeIsObject(name: string, value: unknown): asserts value is object;
    static CheckTypeIsNumber(name: string, value: unknown): asserts value is Nullable<number>;
    static CheckNotNullAndTypeIsNumber(name: string, value: unknown): asserts value is number;
    static CheckTypeIsBoolean(name: string, value: unknown): asserts value is Nullable<boolean>;
    static CheckNotNullAndTypeIsBoolean(name: string, value: unknown): asserts value is boolean;
    static CheckTypeIsFunction(name: string, value: unknown): asserts value is Nullable<AnyFunction>;
    static CheckNotNullAndTypeIsFunction(name: string, value: unknown): asserts value is AnyFunction;
    static CheckTypeIsJQueryObject(name: string, value: unknown): asserts value is Nullable<JQuery>;
    static CheckNotNullAndTypeIsJQuery(name: string, value: unknown): asserts value is JQuery;
}
declare class BrowserUtility {
    static GetIEVersion(): number;
}
declare class PageUtility {
    static Instance: PageUtility;
    private _resizeHandlers;
    private _resizeTimeoutID;
    private _resizeTimeoutInMilliSeconds;
    constructor();
    RegisterResizeHandler(selector: string, handler: PageUtility_ResizeHandler): void;
    PrepareExecuteResizeHandlers(): void;
    ExecuteResizeHandlers(): void;
    IsInDom(element: HTMLElement): boolean;
}
declare type PageUtility_ResizeHandler = (element: JQuery) => void;
declare class PageUtility_ResizeHandlerItem {
    readonly Selector: string;
    Handler: PageUtility_ResizeHandler;
    constructor(selector: string, handler: PageUtility_ResizeHandler);
}
declare type WebServiceProxyInvokeFunction<TResult, TUserContext> = (servicePath: string, methodName: string, useGet?: boolean, params?: Nullable<Dictionary<string>>, onSuccess?: (result: TResult, userContext: TUserContext, methodName: string) => void, onFailure?: (error: Sys.Net.WebServiceError, userContext: TUserContext, methodName: string) => void, userContext?: TUserContext, timeout?: number, enableJsonp?: boolean, jsonpCallbackParameter?: string) => Sys.Net.WebRequest;
declare class WebServiceUtility {
    static Execute<TResult>(serviceUrl: string, serviceMethod: string, params: Dictionary<string>, onSuccess: (result: TResult) => void, onError: (err: Sys.Net.WebServiceError) => void): void;
}
declare class ElementResolverUtility {
    static ResolveSingle<TElement extends Element>(selectorOrElement: CssSelectorOrElement<TElement>, context?: ParentNode): TElement;
    static ResolveMultiple<TElement extends Element>(selectorOrElements: CssSelectorOrElements<TElement>, context?: ParentNode): TElement[];
}
declare class LayoutUtility {
    static GetHeight(element: Element): number;
    static GetWidth(element: Element): number;
    static GetOffset(element: HTMLElement): {
        left: number;
        top: number;
    };
    static GetOuterHeight(element: HTMLElement): number;
    static IsVisible(element: HTMLElement): boolean;
}
