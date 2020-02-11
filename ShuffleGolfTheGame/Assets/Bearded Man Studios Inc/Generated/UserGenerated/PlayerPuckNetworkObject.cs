using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0,0,0]")]
	public partial class PlayerPuckNetworkObject : NetworkObject
	{
		public const int IDENTITY = 6;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		[ForgeGeneratedField]
		private Color _characterColor;
		public event FieldEvent<Color> characterColorChanged;
		public Interpolated<Color> characterColorInterpolation = new Interpolated<Color>() { LerpT = 0f, Enabled = false };
		public Color characterColor
		{
			get { return _characterColor; }
			set
			{
				// Don't do anything if the value is the same
				if (_characterColor == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_characterColor = value;
				hasDirtyFields = true;
			}
		}

		public void SetcharacterColorDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_characterColor(ulong timestep)
		{
			if (characterColorChanged != null) characterColorChanged(_characterColor, timestep);
			if (fieldAltered != null) fieldAltered("characterColor", _characterColor, timestep);
		}
		[ForgeGeneratedField]
		private int _StrokeCount;
		public event FieldEvent<int> StrokeCountChanged;
		public Interpolated<int> StrokeCountInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int StrokeCount
		{
			get { return _StrokeCount; }
			set
			{
				// Don't do anything if the value is the same
				if (_StrokeCount == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_StrokeCount = value;
				hasDirtyFields = true;
			}
		}

		public void SetStrokeCountDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_StrokeCount(ulong timestep)
		{
			if (StrokeCountChanged != null) StrokeCountChanged(_StrokeCount, timestep);
			if (fieldAltered != null) fieldAltered("StrokeCount", _StrokeCount, timestep);
		}
		[ForgeGeneratedField]
		private int _HoleScore;
		public event FieldEvent<int> HoleScoreChanged;
		public Interpolated<int> HoleScoreInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int HoleScore
		{
			get { return _HoleScore; }
			set
			{
				// Don't do anything if the value is the same
				if (_HoleScore == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_HoleScore = value;
				hasDirtyFields = true;
			}
		}

		public void SetHoleScoreDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_HoleScore(ulong timestep)
		{
			if (HoleScoreChanged != null) HoleScoreChanged(_HoleScore, timestep);
			if (fieldAltered != null) fieldAltered("HoleScore", _HoleScore, timestep);
		}
		[ForgeGeneratedField]
		private bool _Finished;
		public event FieldEvent<bool> FinishedChanged;
		public Interpolated<bool> FinishedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool Finished
		{
			get { return _Finished; }
			set
			{
				// Don't do anything if the value is the same
				if (_Finished == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_Finished = value;
				hasDirtyFields = true;
			}
		}

		public void SetFinishedDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_Finished(ulong timestep)
		{
			if (FinishedChanged != null) FinishedChanged(_Finished, timestep);
			if (fieldAltered != null) fieldAltered("Finished", _Finished, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			characterColorInterpolation.current = characterColorInterpolation.target;
			StrokeCountInterpolation.current = StrokeCountInterpolation.target;
			HoleScoreInterpolation.current = HoleScoreInterpolation.target;
			FinishedInterpolation.current = FinishedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _characterColor);
			UnityObjectMapper.Instance.MapBytes(data, _StrokeCount);
			UnityObjectMapper.Instance.MapBytes(data, _HoleScore);
			UnityObjectMapper.Instance.MapBytes(data, _Finished);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_characterColor = UnityObjectMapper.Instance.Map<Color>(payload);
			characterColorInterpolation.current = _characterColor;
			characterColorInterpolation.target = _characterColor;
			RunChange_characterColor(timestep);
			_StrokeCount = UnityObjectMapper.Instance.Map<int>(payload);
			StrokeCountInterpolation.current = _StrokeCount;
			StrokeCountInterpolation.target = _StrokeCount;
			RunChange_StrokeCount(timestep);
			_HoleScore = UnityObjectMapper.Instance.Map<int>(payload);
			HoleScoreInterpolation.current = _HoleScore;
			HoleScoreInterpolation.target = _HoleScore;
			RunChange_HoleScore(timestep);
			_Finished = UnityObjectMapper.Instance.Map<bool>(payload);
			FinishedInterpolation.current = _Finished;
			FinishedInterpolation.target = _Finished;
			RunChange_Finished(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _characterColor);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _StrokeCount);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _HoleScore);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Finished);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (characterColorInterpolation.Enabled)
				{
					characterColorInterpolation.target = UnityObjectMapper.Instance.Map<Color>(data);
					characterColorInterpolation.Timestep = timestep;
				}
				else
				{
					_characterColor = UnityObjectMapper.Instance.Map<Color>(data);
					RunChange_characterColor(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (StrokeCountInterpolation.Enabled)
				{
					StrokeCountInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					StrokeCountInterpolation.Timestep = timestep;
				}
				else
				{
					_StrokeCount = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_StrokeCount(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (HoleScoreInterpolation.Enabled)
				{
					HoleScoreInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					HoleScoreInterpolation.Timestep = timestep;
				}
				else
				{
					_HoleScore = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_HoleScore(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (FinishedInterpolation.Enabled)
				{
					FinishedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					FinishedInterpolation.Timestep = timestep;
				}
				else
				{
					_Finished = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_Finished(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (characterColorInterpolation.Enabled && !characterColorInterpolation.current.UnityNear(characterColorInterpolation.target, 0.0015f))
			{
				_characterColor = (Color)characterColorInterpolation.Interpolate();
				//RunChange_characterColor(characterColorInterpolation.Timestep);
			}
			if (StrokeCountInterpolation.Enabled && !StrokeCountInterpolation.current.UnityNear(StrokeCountInterpolation.target, 0.0015f))
			{
				_StrokeCount = (int)StrokeCountInterpolation.Interpolate();
				//RunChange_StrokeCount(StrokeCountInterpolation.Timestep);
			}
			if (HoleScoreInterpolation.Enabled && !HoleScoreInterpolation.current.UnityNear(HoleScoreInterpolation.target, 0.0015f))
			{
				_HoleScore = (int)HoleScoreInterpolation.Interpolate();
				//RunChange_HoleScore(HoleScoreInterpolation.Timestep);
			}
			if (FinishedInterpolation.Enabled && !FinishedInterpolation.current.UnityNear(FinishedInterpolation.target, 0.0015f))
			{
				_Finished = (bool)FinishedInterpolation.Interpolate();
				//RunChange_Finished(FinishedInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerPuckNetworkObject() : base() { Initialize(); }
		public PlayerPuckNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerPuckNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
