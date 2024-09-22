namespace StatusGrid
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UIElements;

    public class StatusGrid : MonoBehaviour
    {
        public static StatusGrid Instance { get; private set; }

        private Status[,] statuses;

        /// <summary>
        /// this is gonna get called each time a status is changed, and a bunch of times when the status is set
        /// </summary>
        public event System.Action<Vector2Int,Status,Status> StatusChanged;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("more than one StatusGrid Instance.");
            }
            Instance = this;
        }

        private void Start()
        {
            statuses = new Status[MapGrid.Instance.DimensionX, MapGrid.Instance.DimensionY];

            //Debug.Log("status dimensions : " + statuses.GetLength(0) + ", " + statuses.GetLength(1));
        }

        public void SetStatus(StatusType status, Vector2Int position, bool on = true)
        {
            SetStatus(status, position.x,position.y, on);
        }

        public void SetStatus(StatusType status,int x, int y, bool on=true)
        {
            Status before = statuses[x, y];

            Status statusAtPosition = statuses[x,y];
            statusAtPosition.SetStatus(status,on);
            statuses[x, y] = statusAtPosition;

            Status after = statuses[x, y];

            if(before!=after)
            {
                StatusChanged?.Invoke(new Vector2Int(x, y), before, after);
                //Debug.Log("invoked");
            }
        }



        public Status GetStatus(Vector2Int position)
        {
            return GetStatus(position.x,position.y);
        }

        public Status GetStatus(int x, int y)
        {
            return statuses[x, y];
        }



    }

    public struct Status
    {
        public byte statusByte { get; private set; }

        public void SetStatus(byte statusByte)
        {
            this.statusByte = statusByte;
        }

        public void SetStatus(StatusType status, bool on = true)
        {
            byte mask = (byte)(1 << ((int)status));

            if (on)
            {
                statusByte |= mask;
            }
            else
            {
                mask = (byte)~mask;
                statusByte &= mask;
            }
        }

        public bool IsStatus(StatusType status)
        {
            byte mask = (byte)(1 << ((int)status));
            return (statusByte & mask) != 0;
        }

        public static bool operator ==(Status status1, Status status2)
        {
            return status1.statusByte == status2.statusByte;
        }

        public static bool operator !=(Status status1, Status status2)
        {
            return status1.statusByte != status2.statusByte;
        }

        public override bool Equals(object obj)
        {
            try
            {
                var otherStatus = (Status)obj;
                return this.statusByte == otherStatus.statusByte;
            }
            catch{
                return false;
            }
        }
        public override int GetHashCode()
        {
            return statusByte.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("status(");
            foreach (StatusType status in Enum.GetValues(typeof(StatusType)))
            {
                stringBuilder.Append(status.ToString());
                stringBuilder.Append(":");
                stringBuilder.Append(IsStatus(status));
                stringBuilder.Append(",");
            }
            stringBuilder.Append(Convert.ToString(statusByte, toBase: 2));

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }

    public enum StatusType
    {
        NotBuildable = 0,
        NotWalkable = 1,
        Unnamed1 = 2,
        Unnamed2 = 3,

        OnFire = 4,
        Frozen = 5,
        Acid = 6,
        Unnamed3 = 7,
    }
}