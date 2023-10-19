using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using FluentAssertions;
using System;

public class MessengerTests
{
    private Messenger _messenger;

    [SetUp]
    public void SetUp()
    {
        _messenger = new GameObject().AddComponent<Messenger>();
    }

    [Test]
    public void WhenCreatedNewMessage_AndChatAdded_ThenUnreadChatsCountShouldBe1()
    {
        // Arrange.
        MessegeData newMessage = new MessegeData("Dey", ScriptableObject.CreateInstance<DialogData>());
        Chat chat = Substitute.For<Chat>();

        IMessengerWindow messengerWindow = Substitute.For<IMessengerWindow>();
        SaveLoadServise saveLoadServise = Substitute.For<SaveLoadServise>();
        IChatWindow chatWindow = Substitute.For<IChatWindow>();

        messengerWindow.CreateChat(newMessage).Returns(chat);
        chatWindow.ChatRead += Raise.Event<Action<Chat>>(chat);

        _messenger.Construct(saveLoadServise, chatWindow, messengerWindow, new GameObject());

        // Act.
        _messenger.AddNewMessage(newMessage);

        // Assert.
        _messenger.UnreadChats.Count.Should().Be(1);
    }
}