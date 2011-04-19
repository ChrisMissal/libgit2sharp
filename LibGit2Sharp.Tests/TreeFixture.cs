﻿using System.Linq;
using LibGit2Sharp.Tests.TestHelpers;
using NUnit.Framework;

namespace LibGit2Sharp.Tests
{
    [TestFixture]
    public class TreeFixture
    {
        private const string sha = "581f9824ecaf824221bd36edf5430f2739a7c4f5";

        [Test]
        public void TreeDataIsPresent()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup(sha);
                tree.ShouldNotBeNull();
            }
        }

        [Test]
        public void CanReadTheTreeData()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                tree.ShouldNotBeNull();
            }
        }

        [Test]
        public void CanGetEntryCountFromTree()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                var count = tree.Count;
                Assert.That(count, Is.EqualTo(4));
            }
        }

        [Test]
        public void CanGetEntryByIndex()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                tree[0].ShouldNotBeNull();
                tree[1].ShouldNotBeNull();
            }
        }

        [Test]
        public void CanGetShaFromTreeEntry()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                Assert.That(tree[1].Target.Sha, Is.EqualTo("a8233120f6ad708f843d861ce2b7228ec4e3dec6"));
            }
        }

        [Test]
        public void CanGetNameFromTreeEntry()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                Assert.That(tree[1].Name, Is.EqualTo("README"));
            }
        }

        [Test]
        public void CanGetEntryByName()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                Assert.That(tree["README"].Target.Sha, Is.EqualTo("a8233120f6ad708f843d861ce2b7228ec4e3dec6"));
            }
        }

        [Test]
        public void CanConvertEntryToBlob()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                var blob = tree["README"].Object as Blob;
                blob.ShouldNotBeNull();
            }
        }

        [Test]
        public void CanReadEntryAttributes()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                var attr = tree["README"].Attributes;
                Assert.That(attr, Is.EqualTo(33188));
            }
        }

        [Test]
        public void CanConvertEntryToEntry()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                var subtree = tree[0].Object as Tree;
                subtree.ShouldNotBeNull();
            }
        }

        [Test]
        public void CanEnumerateTree()
        {
            using (var repo = new Repository(Constants.TestRepoPath))
            {
                var tree = repo.Lookup<Tree>(sha);
                Assert.That(tree.Count(), Is.EqualTo(tree.Count));
                var list = string.Join(":", tree.Select(te => te.Name).ToArray());
                Assert.That(list, Is.EqualTo("1:README:branch_file.txt:new.txt"));
            }
        }
    }
}