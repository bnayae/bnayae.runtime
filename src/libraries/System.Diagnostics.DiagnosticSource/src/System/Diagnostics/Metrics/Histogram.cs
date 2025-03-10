// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Diagnostics.Metrics
{
    /// <summary>
    /// The histogram is a metrics Instrument which can be used to report arbitrary values that are likely to be statistically meaningful.
    /// e.g. the request duration.
    /// Use <see cref="Meter.CreateHistogram(string, string?, string?)" /> method to create the Histogram object.
    /// </summary>
    /// <remarks>
    /// This class supports only the following generic parameter types: <see cref="byte" />, <see cref="short" />, <see cref="int" />, <see cref="long" />, <see cref="float" />, <see cref="double" />, and <see cref="decimal" />
    /// </remarks>
    public sealed class Histogram<T> : Instrument<T> where T : struct
    {
        internal Histogram(Meter meter, string name, string? unit, string? description) : this(meter, name, unit, description, tags: null)
        {
        }

        internal Histogram(Meter meter, string name, string? unit, string? description, IEnumerable<KeyValuePair<string, object?>>? tags) : base(meter, name, unit, description, tags)
        {
            Publish();
        }

        /// <summary>
        /// using tags builder before record.
        /// </summary>
        /// <typeparam name="TTag"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IHistogramBuilder<T> WithTag<TTag>(string key, TTag value)
        {
            var pair = new KeyValuePair<string, object?>(key, value);
            var list = ImmutableArray.Create(pair);
            return new HistogramBuilder<T>(this, list);
        }

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        public void Record(T value) => RecordMeasurement(value);

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        /// <param name="tag">A key-value pair tag associated with the measurement.</param>
        public void Record(T value, KeyValuePair<string, object?> tag) => RecordMeasurement(value, tag);

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        /// <param name="tag1">A first key-value pair tag associated with the measurement.</param>
        /// <param name="tag2">A second key-value pair tag associated with the measurement.</param>
        public void Record(T value, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2) => RecordMeasurement(value, tag1, tag2);

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        /// <param name="tag1">A first key-value pair tag associated with the measurement.</param>
        /// <param name="tag2">A second key-value pair tag associated with the measurement.</param>
        /// <param name="tag3">A third key-value pair tag associated with the measurement.</param>
        public void Record(T value, KeyValuePair<string, object?> tag1, KeyValuePair<string, object?> tag2, KeyValuePair<string, object?> tag3) => RecordMeasurement(value, tag1, tag2, tag3);

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        /// <param name="tags">A span of key-value pair tags associated with the measurement.</param>
        public void Record(T value, ReadOnlySpan<KeyValuePair<string, object?>> tags) => RecordMeasurement(value, tags);

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        /// <param name="tags">A list of key-value pair tags associated with the measurement.</param>
        public void Record(T value, params KeyValuePair<string, object?>[] tags) => RecordMeasurement(value, tags.AsSpan());

        /// <summary>
        /// Record a measurement value.
        /// </summary>
        /// <param name="value">The measurement value.</param>
        /// <param name="tagList">A <see cref="T:System.Diagnostics.TagList" /> of tags associated with the measurement.</param>
        public void Record(T value, in TagList tagList) => RecordMeasurement(value, in tagList);
    }
}
