// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to Festival Manager (entities/controllers/etc)
// Test ONLY the Stage class. 
namespace FestivalManager.Tests
{
    using NUnit.Framework;
    using System;

	[TestFixture]
	public class StageTests
	{
		private Stage stage;
		private Song song;
		private Performer performer;
		[SetUp]
		public void SetUp()
		{
			stage = new Stage();
			song = new Song("Samo ako bqh", TimeSpan.FromMinutes(3));
			performer = new Performer("Yordan", "Dimitrov", 20);
		}
		[Test]
	    public void TestingConstuctorsAndPropertiest()
	    {

			Assert.That(song.Name, Is.EqualTo("Samo ako bqh"));
			Assert.That(song.Duration, Is.EqualTo(TimeSpan.FromMinutes(3)));
			Assert.That(song.ToString(), Is.EqualTo("Samo ako bqh (03:00)"));

			Assert.That(performer.FullName, Is.EqualTo("Yordan Dimitrov"));
			Assert.That(performer.ToString(), Is.EqualTo("Yordan Dimitrov"));
			Assert.That(performer.Age, Is.EqualTo(20));

			Assert.That(performer.SongList.Count, Is.EqualTo(0));

			Assert.That(stage.Performers.Count, Is.EqualTo(0));
		}

		[Test]
		public void TestingCollectionCountWhenAddingSuccsessfully()
		{
			stage.AddPerformer(performer);
			Assert.That(stage.Performers.Count, Is.EqualTo(1));
		}

		[Test]
		public void TestingCollectionWhenAddingNullPerformer()
		{
			performer = null;
			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(performer));
		}

		[Test]
		public void TestingCollectionWhenAddingYongerPerformer()
		{
			performer = new Performer("Yordan", "Dimitrov", 15);
			Assert.Throws<ArgumentException>(() => stage.AddPerformer(performer));
		}

		//[Test]
		//public void TestingCollectionWhenAddingExistingPerformer()
		//{ 
		//}

		[Test]
		public void TestingCollectionWhenAddingNullableSong()
        {
			song = null;
			Assert.Throws<ArgumentNullException>(() => stage.AddSong(song));
        }

		[Test]
		public void TestingCollectionWhenShorterTimeSong()
		{
			song = new Song("Samo ako bqh", TimeSpan.FromMinutes(0));
			Assert.Throws<ArgumentException>(() => stage.AddSong(song));
		}

		[Test]
		public void TestingStageWhenAddingPerformerAndSongs()
		{
			stage.AddPerformer(performer);
			stage.AddSong(song);

			Assert.That(() => stage.AddSongToPerformer("Samo ako bqh", performer.FullName), Is.EqualTo($"{song.ToString()} will be performed by {performer.FullName}"));
		}

		[Test]
		public void TestingStageWhenAddingPerformerAndSongsWhenPerformerIsMissing()
		{
			stage.AddSong(song);

			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("Samo ako bqh", performer.FullName));
		}

		[Test]
		public void TestingStageWhenAddingPerformerAndSongsWhenSongIsMissing()
		{
			stage.AddPerformer(performer);
			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("Samo ako bqh", performer.FullName));
		}

		[Test]
		public void TestingStageWhenPlaySongs()
		{
			stage.AddPerformer(performer);
			stage.AddSong(song);
			stage.AddSongToPerformer(song.Name, performer.FullName);
			Assert.That($"{stage.Performers.Count} performers played {1} songs", Is.EqualTo(stage.Play()));
		}
	}
}