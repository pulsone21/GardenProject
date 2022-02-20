using UnityEngine;

namespace GridSystem
{
    public abstract class GridObject : IGridDebug
    {
        protected PlaceableObjectSO placeableObject;
        protected GameObject placedGameobject;

        public virtual void SetPlaceableObject(PlaceableObjectSO placeableObject, GameObject gameObject)
        {
            this.placeableObject = placeableObject;
            this.placedGameobject = gameObject;
        }

        public virtual void ClearPlaceableObject()
        {
            this.placeableObject = null;
            this.placedGameobject = null;
        }

        public PlaceableObjectSO GetPlaceableObject() => this.placeableObject;

        public GameObject GetGameObject() => this.placedGameobject;

        public bool CanBuild() => placeableObject == null;

        public virtual void CreateDebugText()
        {
            throw new System.NotImplementedException();
        }

        public virtual void ToogleDebugText()
        {
            throw new System.NotImplementedException();
        }

        public virtual void UpdateDebugText(string newText)
        {
            throw new System.NotImplementedException();
        }
    }


    public class GridObjectXZ : GridObject
    {
        private GridXZ<GridObjectXZ> grid;
        private int x, z;

        public GridObjectXZ(int x, int z, GridXZ<GridObjectXZ> grid)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        public override void ClearPlaceableObject()
        {
            base.ClearPlaceableObject();
            UpdateDebugText(ToString());
        }

        public override void SetPlaceableObject(PlaceableObjectSO PlaceableObject, GameObject gameObject)
        {
            base.SetPlaceableObject(PlaceableObject, gameObject);
            UpdateDebugText(ToString());
        }

        public override string ToString()
        {
            return x + "," + z + "\n" + placeableObject?.name;
        }
    }
}