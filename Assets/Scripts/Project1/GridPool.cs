namespace Project1
{
    public class GridPool : ObjectPoolMB<Grid>
    {
        protected override void ResetObjectDefaults(Grid pooledObject)
        {
            base.ResetObjectDefaults(pooledObject);
            pooledObject.Reset();
        }
    }
}


