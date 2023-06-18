public interface IDUXVisitor
{
    void Visit(CharacterCategoryData characterCategoryData);
    void Visit(CharacterImageVariationCategoryData characterImageVariationCategoryData);
    void Visit(CharacterImageCategoryData characterImageCategoryData);
    void Visit(DiscriptionWithImageData discriptionWithImageData);
    void Visit(MelodyCategoryData melodyCategoryData);
}
