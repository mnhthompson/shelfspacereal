namespace ShelfSpace;

public class MediaState {

    public MediaItem MediaItem { get; private set; } = new MediaItem();

   public void ResetMedia(){
        MediaItem = new MediaItem();
   }

}