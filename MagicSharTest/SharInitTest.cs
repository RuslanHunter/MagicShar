using MagicShar.Models;

namespace MagicSharTest
{
    public class Tests
    {
        /// <summary>
        /// �������� ������ �� ���������� ������ ������������.
        /// </summary>
        [Test]
        public void MagicSharQuestionInValid()
        {

            var res = MagicBall.GetAnswer("� ���� �������� ������?");

            Assert.Contains(res, new[] {
                "��",
                "���",
                "������ ����� ��",
                "������ ����� ���",
                "��������",
                "������� �����������",
            });
        }

        /// <summary>
        /// �������� �� ������ ������.
        /// </summary>
        [Test]
        public void MagicSharQuestionEmpty()
        {

            var res = MagicBall.GetAnswer("");

            Assert.That(res, Is.EqualTo("������ ����� �������!"));
        }

        /// <summary>
        /// �������� �� null.
        /// </summary>
        [Test]
        public void MagicSharQuestionNull()
        {

            var res = MagicBall.GetAnswer(null);

            Assert.That(res, Is.EqualTo("������ ����� �������!"));
        }


        /// <summary>
        /// �������� �� �������.
        /// </summary>
        [Test]
        public void MagicSharQuestionSpace()
        {

            var res = MagicBall.GetAnswer("         ");

            Assert.That(res, Is.EqualTo("������ ����� �������!"));
        }
    }
}