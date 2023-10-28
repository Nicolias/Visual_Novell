using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using FluentAssertions;
using System;
using Factory.Messenger;

public class MessengerTests
{
    private MessengerWindow _messengerWindow;

    private ContactFactory _contactFactory;
    private ChatFactory _chatFactory;

    [SetUp]
    public void SetUp()
    {
        _contactFactory = Substitute.For<ContactFactory>();
        _chatFactory = Substitute.For<ChatFactory>();

        _messengerWindow = CreateGameObjectWith<MessengerWindow>();
        _messengerWindow.Construct(_contactFactory, _chatFactory);
    }

    [Test]
    public void WhenSendNewMessage_AndChatAddedToMessenger_ThenUnreadChatsCountShouldBe1()
    {
        // Arrange.
        Messenger messenger = CreateGameObjectWith<Messenger>();

        MessegeData newMessage = CreateMessage("Dey");
        Chat chat = Substitute.For<Chat>();

        IMessengerWindow messengerWindow = Substitute.For<IMessengerWindow>();
        messengerWindow.CreateChat(newMessage).Returns(chat);

        IChatWindow chatWindow = Substitute.For<IChatWindow>();
        chatWindow.ChatRead += Raise.Event<Action<Chat>>(chat);

        SaveLoadServise saveLoadServise = Substitute.For<SaveLoadServise>();

        messenger.Construct(saveLoadServise, chatWindow, messengerWindow, new GameObject());

        // Act.
        messenger.AddNewMessage(newMessage);

        // Assert.
        messenger.UnreadChats.Count.Should().Be(1);
    }

    [Test]
    public void WhenCreatingChats_AndInMessengerWindowItNewContacts_ThenChatsCountInCountactShouldBe1()
    {
        // Arrange.
        MessegeData oldMessage = CreateMessage("Dey");
        MessegeData newMessage = CreateMessage("Yammy");

        ContactViewInMessenger contactView;
        SendMessageToMessangerWindow(oldMessage);

        // Act.
        contactView = SendMessageToMessangerWindow(newMessage);

        // Assert.
        contactView.ChatsCount.Should().Be(1);
    }

    [Test]
    public void WhenCreatingChats_AndInMessengerWindowItOneContacts_ThenChatsCountInCountactShouldBe2()
    {
        // Arrange.
        MessegeData oldMessage = CreateMessage("Dey");
        MessegeData newMessage = CreateMessage("Dey");

        ContactViewInMessenger contactView;
        SendMessageToMessangerWindow(oldMessage);

        // Act.
        contactView = SendMessageToMessangerWindow(newMessage);

        // Assert.
        contactView.ChatsCount.Should().Be(2);
    }

    private ContactViewInMessenger SendMessageToMessangerWindow(MessegeData newMessage)
    {
        if (_messengerWindow.TryGetContactView(newMessage.ContactName, out ContactViewInMessenger contactView) == false)
            contactView = CreateGameObjectWith<ContactViewInMessenger>();

        _contactFactory.CreateNewContactView(new ContactData(newMessage.ContactName)).ReturnsForAnyArgs(contactView);

        Chat chat = Substitute.For<Chat>();
        chat.Data.Returns(newMessage.Messege);
        _chatFactory.Create(newMessage.Messege, contactView.ChatsContainer).Returns(chat);

        _messengerWindow.CreateChat(newMessage);

        return contactView;
    }

    private MessegeData CreateMessage(string contactName)
    {
        return new MessegeData(contactName, ScriptableObject.CreateInstance<DialogData>());
    }

    private T CreateGameObjectWith<T>() where T : MonoBehaviour
    {
        return new GameObject().AddComponent<T>();
    }
}
