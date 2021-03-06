using System.Collections.Generic;
using System.Linq;

namespace RE
{
	partial class CharFA<TAccept>
	{
		public class InputTransitionsDictionary
		{
			private Dictionary<char, CharFA<TAccept>> _charactersTransitions = new Dictionary<char, CharFA<TAccept>>();
			private List<(CharRange range, CharFA<TAccept> fa)> _rangeTransitions = new List<(CharRange range, CharFA<TAccept> fa)>();
			private Dictionary<CharFA<TAccept>,(ICollection<char> characters, ICollection<CharRange> ranges)> _charactersByState = new Dictionary<CharFA<TAccept>, (ICollection<char> characters, ICollection<CharRange> ranges)>();

			public IEnumerable<KeyValuePair<char, CharFA<TAccept>>> CharactersTransitions => _charactersTransitions;
			public IEnumerable<(CharRange range, CharFA<TAccept> fa)> RangesTransitions => _rangeTransitions;
			public IReadOnlyDictionary<CharFA<TAccept>,(ICollection<char> characters, ICollection<CharRange> ranges)> CharactersByState => _charactersByState;

			public int Count => _charactersTransitions.Count + _rangeTransitions.Count;

			public bool TryGetValue(char input, out CharFA<TAccept> fa)
			{
				if (_charactersTransitions.TryGetValue(input, out fa))
					return true;
				foreach (var rangeTransition in _rangeTransitions)
				{
					if (rangeTransition.range.First <= input && input <= rangeTransition.range.Last)
					{
						fa = rangeTransition.fa;
						return true;
					}
				}

				return false;
			}

			public void Add(char input, CharFA<TAccept> fa)
			{
				_charactersTransitions.Add(input, fa);
				if (!_charactersByState.TryGetValue(fa, out var chars))
				{
					chars = (new List<char>(), new List<CharRange>());
					_charactersByState[fa] = chars;
				}
				chars.characters.Add(input);
			}

			public void Add(CharRange inputRange, CharFA<TAccept> fa)
			{
				_rangeTransitions.Add((inputRange, fa));
				if (!_charactersByState.TryGetValue(fa, out var chars))
				{
					chars = (new List<char>(), new List<CharRange>());
					_charactersByState[fa] = chars;
;				}
				chars.ranges.Add(inputRange);
			}

			public void Remove(CharFA<TAccept> fa)
			{
				_charactersByState.Remove(fa);
				var keys = _charactersTransitions.Where(x => x.Value == fa).ToList();
				foreach (var key in keys)
					_charactersTransitions.Remove(key.Key);
				var rangeKeys = _rangeTransitions.Where(x => x.fa == fa).Select((x, i) => i).ToList();
				foreach (var i in rangeKeys)
					_rangeTransitions.RemoveAt(i);
			}

			public void Add(CharFA<TAccept> fa, (ICollection<char> characters, ICollection<CharRange> ranges) inputs)
			{
				foreach (var input in inputs.characters)
					Add(input, fa);
				foreach (var inputsRange in inputs.ranges)
					Add(inputsRange, fa);
			}
		}
	}
}