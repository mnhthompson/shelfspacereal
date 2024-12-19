namespace ShelfSpace;

[Route("media")]
[ApiController]

public class UserController : Controller
    {
        private readonly ShelfSpaceContext _context;

        public UserController(ShelfSpaceContext context)
        {
            _context = context;
        }

        private bool UserExists(string SpecialId)
        {
            return _context.User.Any(e => e.Id == SpecialId);
        }

    }

    