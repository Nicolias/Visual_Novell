public interface IDUXVisitor
{
    void Visit(CharacterImageVariationCategoryData characterImageVariationCategoryData);
    void Visit(CharacterImageCategoryData characterImageCategoryData);
    void Visit(DiscriptionWithImageData discriptionWithImageData);
    void Visit(MelodyCategoryData melodyCategoryData);
    void Visit(CharacterCategoryData characterCategoryData);
}
