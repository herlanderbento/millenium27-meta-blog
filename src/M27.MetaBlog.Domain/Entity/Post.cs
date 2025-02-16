using M27.MetaBlog.Domain.Shared;
using M27.MetaBlog.Domain.Validator;
using M27.MetaBlog.Domain.ValueObject;

namespace M27.MetaBlog.Domain.Entity;

public class Post: AggregateRoot
{
    public string Title { get; private set; }
    public Slug Slug { get; private set; }
    public string Description { get; private set; }
    public bool Published { get; private set; }
    public Image? Image { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public Guid AuthorId { get; private set; }
    public Guid CategoryId { get; private set; }
    
    public Post(
        Guid authorId, 
        Guid categoryId,
        string title, 
        string description, 
        bool published
    ) : base()
    {
        AuthorId = authorId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Published = published;
        
        Slug = Slug.CreateFromText(title);

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }


    public void Update(
        Guid? categoryId,
        string? title, 
        string? description, 
        bool? published
        )
    {
        CategoryId = categoryId ?? CategoryId;
        Title = title ?? Title;
        Description = description ?? Description;
        Published = published ?? Published;
        
        UpdatedAt = DateTime.UtcNow;

        Validate();
    }
    
    public void UpdateImage(string path)
        => Image = new Image(path);

    private void Validate()
    {
        PostValidator.Validate(
            AuthorId, 
            CategoryId, 
            Title, 
            Description
        );
    }
}