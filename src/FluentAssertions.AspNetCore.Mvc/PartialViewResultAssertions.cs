﻿using System;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace FluentAssertions.AspNetCore.Mvc
{
    public class PartialViewResultAssertions : ObjectAssertions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:PartialViewResultAssertions" /> class.
        /// </summary>
        /// <param name="subject">The object to test assertion on</param>
        public PartialViewResultAssertions(PartialViewResult subject) : base(subject)
        {
        }

        private PartialViewResult PartialViewResultSubject => (PartialViewResult)Subject;

        /// <summary>
        ///     The model.
        /// </summary>
        public object Model => PartialViewResultSubject.ViewData.Model;

        /// <summary>
        ///     Asserts that the view name is the expected view name.
        /// </summary>
        /// <param name="expectedViewName">The view name.</param>
        /// <param name="reason">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public PartialViewResultAssertions WithViewName(string expectedViewName, string reason = "",
            params object[] reasonArgs)
        {
            var actualViewName = PartialViewResultSubject.ViewName;

            Execute.Assertion
                .ForCondition(string.Equals(expectedViewName, actualViewName, StringComparison.OrdinalIgnoreCase))
                .BecauseOf(reason, reasonArgs)
                .FailWith(FailureMessages.ViewResultBase_ViewName, expectedViewName, actualViewName);
            return this;
        }

        /// <summary>
        ///     Asserts that the view data contains the expected data.
        /// </summary>
        /// <param name="key">The expected view data key.</param>
        /// <param name="expectedValue">The expected view data.</param>
        /// <param name="reason">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public PartialViewResultAssertions WithViewData(string key, object expectedValue, string reason = "",
            params object[] reasonArgs)
        {
            var actualViewData = PartialViewResultSubject.ViewData;

            Execute.Assertion
                .ForCondition(actualViewData.ContainsKey(key))
                .BecauseOf(reason, reasonArgs)
                .FailWith(FailureMessages.ViewResultBase_ViewData_ContainsKey, key);

            var actualValue = actualViewData[key];

            Execute.Assertion
                .ForCondition(actualValue.Equals(expectedValue))
                .BecauseOf(reason, reasonArgs)
                .FailWith(FailureMessages.ViewResultBase_ViewData_HaveValue, key, expectedValue, actualValue);

            return this;
        }

        /// <summary>
        ///     Asserts that the temp data contains the expected data.
        /// </summary>
        /// <param name="key">The expected temp data key.</param>
        /// <param name="expectedValue">The expected temp data.</param>
        /// <param name="reason">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public PartialViewResultAssertions WithTempData(string key, object expectedValue, string reason = "",
            params object[] reasonArgs)
        {
            var actualTempData = PartialViewResultSubject.TempData;

            Execute.Assertion
                .ForCondition(actualTempData.ContainsKey(key))
                .BecauseOf(reason, reasonArgs)
                .FailWith("TempData does not contain key of '{0}'", key);

            actualTempData[key].Should().Be(expectedValue);

            return this;
        }

        /// <summary>
        ///     Asserts the model is of the expected type.
        /// </summary>
        /// <typeparam name="TModel">The expected type.</typeparam>
        /// <returns>The typed model.</returns>
        public TModel ModelAs<TModel>()
        {
            var model = PartialViewResultSubject.ViewData?.Model;

            if (model == null)
                Execute.Assertion.FailWith(FailureMessages.CommonNullWasSuppliedFailMessage, "Model", typeof(TModel).Name);

            Execute.Assertion
                .ForCondition(model is TModel)
                .FailWith(FailureMessages.CommonTypeFailMessage, "Model", typeof(TModel).Name, model.GetType().Name);

            return (TModel)model;
        }

        /// <summary>
        ///     Asserts that the default view will be used.
        /// </summary>
        /// <param name="reason">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="reason" />.
        /// </param>
        public PartialViewResultAssertions WithDefaultViewName(string reason = "", params object[] reasonArgs)
        {
            var viewName = PartialViewResultSubject.ViewName;

            Execute.Assertion
                .ForCondition(viewName == string.Empty)
                .BecauseOf(reason, reasonArgs)
                .FailWith(FailureMessages.ViewResultBase_WithDefaultViewName, viewName);

            return this;
        }
    }
}