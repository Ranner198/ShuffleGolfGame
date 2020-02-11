using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class ServerManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _CurrentHoleNumber;
		public event FieldEvent<int> CurrentHoleNumberChanged;
		public Interpolated<int> CurrentHoleNumberInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int CurrentHoleNumber
		{
			get { return _CurrentHoleNumber; }
			set
			{
				// Don't do anything if the value is the same
				if (_CurrentHoleNumber == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_CurrentHoleNumber = value;
				hasDirtyFields = true;
			}
		}

		public void SetCurrentHoleNumberDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_CurrentHoleNumber(ulong timestep)
		{
			if (CurrentHoleNumberChanged != null) CurrentHoleNumberChanged(_CurrentHoleNumber, timestep);
			if (fieldAltered != null) fieldAltered("CurrentHoleNumber", _CurrentHoleNumber, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			CurrentHoleNumberInterpolation.current = CurrentHoleNumberInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _CurrentHoleNumber);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_CurrentHoleNumber = UnityObjectMapper.Instance.Map<int>(payload);
			CurrentHoleNumberInterpolation.current = _CurrentHoleNumber;
			CurrentHoleNumberInterpolation.target = _CurrentHoleNumber;
			RunChange_CurrentHoleNumber(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _CurrentHoleNumber);

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
				if (CurrentHoleNumberInterpolation.Enabled)
				{
					CurrentHoleNumberInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					CurrentHoleNumberInterpolation.Timestep = timestep;
				}
				else
				{
					_CurrentHoleNumber = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_CurrentHoleNumber(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (CurrentHoleNumberInterpolation.Enabled && !CurrentHoleNumberInterpolation.current.UnityNear(CurrentHoleNumberInterpolation.target, 0.0015f))
			{
				_CurrentHoleNumber = (int)CurrentHoleNumberInterpolation.Interpolate();
				//RunChange_CurrentHoleNumber(CurrentHoleNumberInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public ServerManagerNetworkObject() : base() { Initialize(); }
		public ServerManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public ServerManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
